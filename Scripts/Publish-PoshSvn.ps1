param (
    [string]$NuGetApiKey
)
    
Publish-Module -Path "$PSScriptRoot\..\bin\poshsvn" -NuGetApiKey $NuGetApiKey
