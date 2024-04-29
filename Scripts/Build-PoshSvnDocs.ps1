[CmdletBinding()]
param (
    [Parameter(Mandatory)]
    [string]
    $InputDir,

    [Parameter(Mandatory)]
    [string]
    $OutputDir
)

Import-Module -Name "$OutputDir\platyPS.psd1" -Force

foreach ($path in Get-ChildItem $InputDir -Exclude obj -Directory) {
    $outputPath = "$OutputDir\$($path.Name)"

    Remove-Item $outputPath -Force -Recurse -ErrorAction SilentlyContinue

    New-ExternalHelp -Path "$path\Cmdlets" -OutputPath $outputPath -Force

    Get-ChildItem $path -Exclude "Cmdlets" -Recurse -Directory | Get-ChildItem -Filter "*.md" | ForEach-Object {
        New-ExternalHelp -Path $_.FullName -OutputPath $outputPath -Force
    }
}
