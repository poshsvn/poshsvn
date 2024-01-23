# TODO: Correct filename
$exeFileName = "C:\Program Files\TortoiseSVN\bin\svn.exe"

$parameters = @{
    Verb = 'Get'
    Noun = 'SvnStatus'
    OriginalName = $exeFileName
}

$newCommand = New-CrescendoCommand @parameters
$newCommand.OriginalCommandElements = @('status')

$CrescendoCommands += $newCommand

Export-CrescendoCommand -command $CrescendoCommands -fileName .\out\Svn.json -Force

if (!(Test-Path .\out\)) {
    $null = mkdir .\out\
}

Export-CrescendoModule -ConfigurationFile .\out\Svn.json -ModuleName .\out\Svn.psm1 -Force
