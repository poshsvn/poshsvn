[CmdletBinding()]
param (
    [string]$Configuration = "Release"
)

Write-Verbose "Restoring nuget packages..."
dotnet.exe msbuild $SolutionPath -restore

Write-Verbose "Building project..."
dotnet.exe msbuild $SolutionPath /p:Configuration=$Configuration /v:normal
