[CmdletBinding()]
param (
    [string]$Configuration = "Release"
)

dotnet.exe msbuild "$PSScriptRoot\.." /restore /p:Configuration=$Configuration /v:normal
