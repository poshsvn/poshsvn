# TODO: Correct filename
$exeFileName = "C:\Program Files\TortoiseSVN\bin\svn.exe"

$parameters = @{
    Verb         = 'Get'
    Noun         = 'SvnStatus'
    OriginalName = $exeFileName
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

        $null = $line -match "-([a-z]|-)*"
        $parameterName = $Matches[0]

        $null = $line -match ": (.*)"
        $description = $Matches[1]

        $isParameter = $line -match "ARG"

        $parameters +=  @{
            ParameterName = $parameterName
            Description   = $description
            Type = $isParameter ? "string" : "switch"
        }
    }
    else {
        $null = $line -match " {29}(.*)"
        $description = $Matches[1]
        $parameters[-1].Description += "`n$description"
    }

    $i++
}

foreach ($parameter in $parameters) {
    $newParameter = New-ParameterInfo -OriginalName $parameter.ParameterName -Name $parameter.ParameterName.Replace("-", "")

    $newParameter.ParameterType = $parameter.Type
    $newParameter.NoGap = $false
    $newParameter.Description = $parameter.Description
    $newCommand.Parameters += $newParameter
}

$CrescendoCommands += $newCommand

if (!(Test-Path .\out\)) {
    $null = mkdir .\out\
}

Export-CrescendoCommand -command $CrescendoCommands -fileName .\out\Svn.json -Force

Export-CrescendoModule -ConfigurationFile .\out\Svn.json -ModuleName .\out\Svn.psm1 -Force
