[CmdletBinding()]
param (
    [Parameter(Mandatory)]
    [string]
    $InputDir,

    [Parameter(Mandatory)]
    [string]
    $OutputDir
)

if (Get-Module -ListAvailable -Name platyPS) {
    "platyPS is already installed." 
}
else {
    "Importing PowerShellGet."
    Import-Module -Name PowerShellGet -Force -Function Install-Module -Scope Local

    "Installing platyPS."
    Install-Module -Name platyPS -Force -Scope CurrentUser -ErrorAction Stop
    "platyPS has installed successfully."
}

foreach ($path in Get-ChildItem $InputDir -Exclude obj -Directory) {
    New-ExternalHelp -Path $path -OutputPath $OutputDir\$($path.Name) -Force
}
