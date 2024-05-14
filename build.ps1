[CmdletBinding()]
param (
    [Parameter()]
    [ValidateSet("Core", "Installer", "All", "SvnDist", "platyPS")]
    [string]
    $Target = "All"
)

if ($Target -eq "All") {
    $msbuildTarget = "build"
}
elseif ($Target -eq "Core") {
    $msbuildTarget = "PoshSvn"
}
else {
    $msbuildTarget = $Target
}

$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$installationPath = & $vswhere -property "installationPath"
$msbuild = "$installationPath\MSBuild\Current\Bin\MSBuild.exe"

& $msbuild /property:Configuration=Release /t:$msbuildTarget /restore
