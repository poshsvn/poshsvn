[CmdletBinding()]
param (
    [Parameter()]
    [ValidateSet("Core", "Installer", "All", "SvnDist", "platyPS", "Package")]
    [string]
    $Target = "All",

    # Launches the installer after build.
    [Parameter()]
    [switch]
    $Install,

    [Parameter()]
    [ValidateSet("Release", "Debug")]
    [string]
    $Configuration = "Release",

    [Parameter()]
    [ValidateSet("x64", "x86")]
    [string]
    $Platform = "x64"
)

if ($Target -ne "All" -and $Target -ne "Installer" -and $Install) {
    throw "Cannot install without building installer. Please specify -Target to 'All' or 'Installer'."
}

if ($Target -eq "All") {
    $msbuildTarget = "build"
}
elseif ($Target -eq "Core") {
    $msbuildTarget = "PoshSvn"
}
elseif ($Target -eq "Package") {
    $msbuildTarget = "PoshSvn_Package"
}
else {
    $msbuildTarget = $Target
}

$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$installationPath = & $vswhere -property "installationPath"
$msbuild = "$installationPath\MSBuild\Current\Bin\MSBuild.exe"

& $msbuild /property:Configuration=$Configuration /property:Platform=$Platform `
    /t:$msbuildTarget /restore `
    /fileLogger /fileLoggerParameters:verbosity=normal

if ($Install) {
    msiexec.exe /i bin\Release-x64\Installer\en-US\PoshSvn.msi /qb
}
