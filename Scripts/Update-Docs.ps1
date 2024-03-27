param (
)

$docsPath = "$PSScriptRoot\..\PoshSvn.Docs"

& $PSScriptRoot\Build-PoshSvn.ps1
Import-Module $PSScriptRoot\..\bin\poshsvn\PoshSvn.psd1 -Force

New-MarkdownHelp -Module PoshSvn -OutputFolder $docsPath  -ExcludeDontShow -AlphabeticParamsOrder -ErrorAction SilentlyContinue

svn-add $docsPath -Force

Update-MarkdownHelp -Path $docsPath -ExcludeDontShow -AlphabeticParamsOrder
