[CmdletBinding()]
param (
    [string]$Configuration = "Release"
)

$Output = "$PSScriptRoot\..\bin\poshsvn"
$rootPath = "$PSScriptRoot\.."
$SolutionPath = "$rootPath\PoshSvn.sln"

Remove-Item $Output -Recurse -Force -ErrorAction Ignore

Write-Verbose "Restoring nuget packages..."
dotnet.exe msbuild $SolutionPath /t:PoshSvn -restore

Write-Verbose "Building project..."
dotnet.exe msbuild $SolutionPath /t:PoshSvn,docs /p:Configuration=$Configuration /p:OutputPath=$Output /v:normal

Compress-Archive -Path $Output\* -DestinationPath "$Output.zip" -Force -CompressionLevel Optimal
