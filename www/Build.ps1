Set-Location $PSScriptRoot

Remove-Item -Recurse -Force .\files -ErrorAction SilentlyContinue
mkdir .\files

dotnet msbuild clean ..
dotnet msbuild .. /target:PoshSvn /p:Configuration=Release /p:Platform=x64

Compress-Archive ..\bin\Release\* .\files\poshsvn-win-x64
