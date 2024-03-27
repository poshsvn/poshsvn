[CmdletBinding()]
param (
    [string]$Configuration = "Release"
)

dotnet.exe msbuild $SolutionPath /restore /p:Configuration=$Configuration /v:normal
