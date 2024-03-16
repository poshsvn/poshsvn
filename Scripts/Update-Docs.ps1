param (
)

& $PSScriptRoot\Build-PoshSvn.ps1
Import-Module $PSScriptRoot\..\bin\poshsvn\PoshSvn.psd1 -Force
Update-MarkdownHelp -Path $PSScriptRoot\..\docs -ExcludeDontShow -AlphabeticParamsOrder
