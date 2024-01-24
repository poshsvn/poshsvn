# TODO: Correct filename
$exeFileName = "C:\Program Files\TortoiseSVN\bin\svn.exe"

$parameters = @{
    Verb         = 'Get'
    Noun         = 'SvnStatus'
    OriginalName = $exeFileName
}

function FormatParameter {
    param (
        [string]$ParameterName
    )
    $ParameterName.Replace("-", "").Replace("verbose", "full")
}

$newCommand = New-CrescendoCommand @parameters
$newCommand.OriginalCommandElements = @('status')

$out = svn.exe help status

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
            ParameterNames    = $parameterNames
            Description       = $description
            Type              = $isParameter ? "string" : "switch"
        }
    }
    else {
        $null = $line -match " {29}(.*)"
        $description = $Matches[1]
        $parameters[-1].Description += "`n$description"
    }

    $i++
}

$parameters | Format-List

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

$CrescendoCommands = @($newCommand)

if (!(Test-Path .\out\)) {
    $null = mkdir .\out\
}

Export-CrescendoCommand -command $CrescendoCommands -fileName .\out\Svn.json -Force

Export-CrescendoModule -ConfigurationFile .\out\Svn.json -ModuleName .\out\Svn.psm1 -Force
