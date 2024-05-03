param (
)

$docsPath = "$PSScriptRoot\..\PoshSvn.Docs\en-US\Cmdlets"

Import-Module "$PSScriptRoot\..\bin\poshsvn\PoshSvn.psd1" -Force
Import-Module "$PSScriptRoot\..\bin\Debug-x64\platyPS.psd1" -Force

New-MarkdownHelp -Module PoshSvn -OutputFolder $docsPath  -ExcludeDontShow -AlphabeticParamsOrder -ErrorAction SilentlyContinue

svn-add $docsPath -Force

Update-MarkdownHelp -Path $docsPath -ExcludeDontShow -AlphabeticParamsOrder
