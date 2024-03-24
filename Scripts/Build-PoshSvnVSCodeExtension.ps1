$extensionRoot = "$PSScriptRoot\..\PoshSvn.vscode"
$outDir = "$extensionRoot\out"

Push-Location $extensionRoot

Remove-Item -Recurse -Force $outDir -ErrorAction SilentlyContinue

& npm install
& tsc
& "$PSScriptRoot\Build-PoshSvn.ps1"
Copy-Item "$PSScriptRoot\..\bin\poshsvn\*" $outDir -Recurse -Force

Pop-Location
