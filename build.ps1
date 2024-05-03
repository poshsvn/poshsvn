[CmdletBinding()]
param (
    [Parameter()]
    [ValidateSet("Core", "Installer", "All")]
    [string]
    $Target
)

$msbuildTarget = "build"
if ($Target -eq "Core") {
    $msbuildTarget = "PoshSvn"
}
if ($Target -eq "Installer") {
    $msbuildTarget = "Installer"
}

$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$installationPath = & $vswhere -property "installationPath"
$msbuild = "$installationPath\MSBuild\Current\Bin\MSBuild.exe"

& $msbuild /property:Configuration=Release /t:$msbuildTarget
