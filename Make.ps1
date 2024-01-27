if (!(Test-Path "$PSScriptRoot\out\")) {
    $null = mkdir "$PSScriptRoot\out\"
}
if (!(Test-Path "$PSScriptRoot\tmp\")) {
    $null = mkdir "$PSScriptRoot\tmp\"
}
if (!(Test-Path "$PSScriptRoot\tmp\subversion.zip")) {
    Write-Host "Downloading Subversion..."
    $url = "https://www.visualsvn.com/files/Apache-Subversion-1.14.3.zip"
    Invoke-WebRequest -Uri $url -OutFile "$PSScriptRoot\tmp\subversion.zip"
    Write-Host "Subversion downloaded"
}

if (!(Get-Module -ListAvailable -Name Microsoft.PowerShell.Crescendo)) {
    Write-Host "Installing Crescendo..."
    Install-Module Microsoft.PowerShell.Crescendo -Force
    Write-Host "Crescendo Installed"
}

Write-Verbose "Coping Subversion to 'out' directory..."
Expand-Archive -Path "$PSScriptRoot\tmp\subversion.zip" -DestinationPath "$PSScriptRoot\out\subversion" -Force
Write-Verbose "Coping Subversion to 'out' directory finished"

$subversionPath = "$PSScriptRoot\out\subversion\bin\svn.exe"

$parameterMapping = @{
    "verbose" = "full"
    "R"       = "rec"
}

function FormatParameter {
    param (
        [string]$ParameterName
    )

    $ParameterName = $ParameterName.Replace("-", "")

    foreach ($mapping in $parameterMapping.GetEnumerator()) {
        if ($ParameterName -ceq $mapping.Key) {
            $ParameterName = $mapping.Value
        }
    }

    $ParameterName
}

function NewCommand {
    param (
        [string]$CommandName
    )

    $parameters = @{
        Verb         = 'Invoke'
        Noun         = 'Svn' + (Get-Culture).TextInfo.ToTitleCase($CommandName)
        OriginalName = "`$PSScriptRoot\subversion\bin\svn.exe"
    }

    $newCommand = New-CrescendoCommand @parameters
    $newCommand.Aliases += "svn-" + $CommandName
    $newCommand.OriginalCommandElements = @($CommandName)
    
    $out = & $subversionPath help $CommandName
    
    $i = 0;
    while ($i -lt $out.Length -and $out[$i] -ne "Valid options:") {
        $i++
    }
    $i++
    $parameters = @()
    while ($i -lt $out.Length) {
        $line = $out[$i]
        if ($line -match " : ") {
            $parameterNames = @()

            if ($line -match " -([a-z]([a-z]|-)*)") {
                $parameterNames += FormatParameter $Matches[1]
            }
            if ($line -match "--([a-z]([a-z]|-)*)") {
                $parameterNames += FormatParameter $Matches[1]
            }

            $null = $line -match ": (.*)"
            $description = $Matches[1]

            $isParameter = $line -match "ARG"

            $parameters += @{
                ParameterNames = $parameterNames
                Description    = $description
                Type           = $isParameter ? "string" : "switch"
            }
        }
        else {
            $null = $line -match " {29}(.*)"
            $description = $Matches[1]
            $parameters[-1].Description += "`n$description"
        }

        $i++
    }

    $usageLine = $out[1]

    if ($usageLine -match "URL\[@REV\]\.\.\.") {
        $newParameter = New-ParameterInfo -OriginalName "" -Name "Url"
        $newParameter.Position = 0
        $newParameter.ParameterType = "string[]"
        $newParameter.NoGap = $false
        $newParameter.Description = ""
        $newParameter.Mandatory = $true
        $newParameter.ValueFromPipeline = $true

        $newCommand.Parameters += $newParameter
    }

    if ($usageLine -match "PATH\.\.\.") {
        $newParameter = New-ParameterInfo -OriginalName "" -Name "Path"
        $newParameter.Position = 1
        $newParameter.ParameterType = "string[]"
        $newParameter.NoGap = $false
        $newParameter.Description = ""
        $newParameter.Mandatory = $false # do something ?
        $newParameter.ValueFromPipeline = $true

        $newCommand.Parameters += $newParameter
    }
    elseif ($usageLine -match "\[PATH\]") {
        $newParameter = New-ParameterInfo -OriginalName "" -Name "Path"
        $newParameter.Position = 1
        $newParameter.ParameterType = "string"
        $newParameter.NoGap = $false
        $newParameter.Description = ""
        $newParameter.Mandatory = $false # do something ?
        $newParameter.ValueFromPipeline = $true

        $newCommand.Parameters += $newParameter
    }

    foreach ($parameter in $parameters) {
        $originalName = ($parameter.ParameterNames[0].Length -eq 1) ? "-" + $parameter.ParameterNames[0] : "--" + $parameter.ParameterNames[0]
        $newParameter = New-ParameterInfo -OriginalName $originalName -Name $parameter.ParameterNames[-1]

        $newParameter.ParameterType = $parameter.Type
        $newParameter.NoGap = $false
        $newParameter.Description = $parameter.Description

        for ($i = 0; $i -lt $parameter.ParameterNames.Count - 1; $i++) {
            $newParameter.Aliases += $parameter.ParameterNames[$i]
        }

        $newCommand.Parameters += $newParameter
    }

    $newCommand
}

$CrescendoCommands = @()

$commandList = & $subversionPath "help"

for ($i = 12; $i -lt $commandList.Length - 3; $i++) {
    $null = $commandList[$i] -match "^   ([a-z]+)"
    $command = $Matches[1]
    Write-Progress -Activity "Processing command" -Status "$command" -PercentComplete (($i - 12) / ($commandList.Length - 3 - 12) * 100)
    $CrescendoCommands += NewCommand $command
    Write-Verbose "Command '$command' processed!"
}

Write-Progress -Activity "Generating Code..."
Write-Verbose "Generating Code..."
Export-CrescendoCommand -command $CrescendoCommands -fileName "$PSScriptRoot\tmp\Svn.json" -Force

Export-CrescendoModule -ConfigurationFile "$PSScriptRoot\tmp\Svn.json" -ModuleName "$PSScriptRoot\out\Svn.psm1" -Force
Write-Verbose "Generating Code finished"
