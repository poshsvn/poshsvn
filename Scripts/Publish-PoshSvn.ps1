param (
    [Parameter(Mandatory)]
    [string]$NuGetApiKey
)

& "$PSScriptRoot\Build-PoshSvn"

& "$PSScriptRoot\Test-PoshSvn"

$baseUrl = (svn-info "$PSScriptRoot\..").Url

$data = Import-PowerShellDataFile -Path $PSScriptRoot\..\PoshSvn\PoshSvn.psd1
$tagName = $data.ModuleVersion

if (($baseUrl.Segments | Select-Object -Last 1) -eq "trunk/") {
    $tagUrl = "$baseUrl/../tags/$tagName"
    try {
        svn-copy $baseUrl $tagUrl -maa "Create tag '$tagName'"
    }
    catch {
        $choice = @(
            (New-Object System.Management.Automation.Host.ChoiceDescription("&Yes", "Yes")),
            (New-Object System.Management.Automation.Host.ChoiceDescription("&No", "No"))
        )
        $response = $Host.Ui.PromptForChoice($null, "Cannot create tag. Do you want to continue?", $choice, 0)

        if ($response -ne 0) {
            throw "Operation aborted."
        }
    }
}
else {
    throw "Publishing is now supported only from 'trunk'"
}

Publish-Module -Path "$PSScriptRoot\..\bin\poshsvn" -NuGetApiKey $NuGetApiKey
