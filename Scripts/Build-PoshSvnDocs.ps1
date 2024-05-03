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

    # Generate MAML help file.
    New-ExternalHelp -Path "$path\Cmdlets" -OutputPath $outputPath -Force

    # Build 'about' help.
    New-ExternalHelp -Path "$path\About" -OutputPath $outputPath -Force
}
