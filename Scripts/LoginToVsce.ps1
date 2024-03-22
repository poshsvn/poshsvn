param (
    [Parameter(Mandatory)]
    [string]$Token
)

$Token | vsce login rinrab
