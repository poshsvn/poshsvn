$location = Get-Location
Set-Location $PSScriptRoot\..

$data = Import-PowerShellDataFile -Path $PSScriptRoot\..\PoshSvn\PoshSvn.psd1
$tagName = $data.ModuleVersion
git tag $tagName
git push origin refs/tags/$tagName

Set-Location $location
