# TODO: Correct filename
$exeFileName = "C:\Program Files\TortoiseSVN\bin\svn.exe"

$parameters = @{
    Verb         = 'Get'
    Noun         = 'SvnStatus'
    OriginalName = $exeFileName
}

$newCommand = New-CrescendoCommand @parameters
$newCommand.OriginalCommandElements = @('status')

$parameter = New-ParameterInfo -OriginalName '--show-updates' -Name 'ShowUpdates'
$parameter.ParameterType = 'switch'
$parameter.NoGap = $true
$parameter.Description = "display update information"
$newCommand.Parameters += $parameter

$parameter = New-ParameterInfo -OriginalName '-v' -Name 'v'
$parameter.ParameterType = 'switch'
$parameter.NoGap = $true
$parameter.Description = "print extra information"
$newCommand.Parameters += $parameter

$CrescendoCommands += $newCommand

if (!(Test-Path .\out\)) {
    $null = mkdir .\out\
}

Export-CrescendoCommand -command $CrescendoCommands -fileName .\out\Svn.json -Force

Export-CrescendoModule -ConfigurationFile .\out\Svn.json -ModuleName .\out\Svn.psm1 -Force
