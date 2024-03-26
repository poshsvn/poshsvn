[CmdletBinding()]
param ()

if (Get-Module -ListAvailable -Name platyPS) {
    "platyPS is already installed." 
}
else {
    "Importing PowerShellGet."
    Import-Module -Name PowerShellGet

    "Installing platyPS."
    Install-Module -Name platyPS -Force -Scope CurrentUser -ErrorAction Stop
    "platyPS has installed successfully."
}
