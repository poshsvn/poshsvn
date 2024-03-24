$extensionRoot = "$PSScriptRoot\..\PoshSvn.vscode"

Push-Location $extensionRoot

& npm install
& tsc
& "$PSScriptRoot\Build-PoshSvn.ps1"
Copy-Item "$PSScriptRoot\..\bin\poshsvn\*" "$extensionRoot\out" -Recurse -Force

Pop-Location
