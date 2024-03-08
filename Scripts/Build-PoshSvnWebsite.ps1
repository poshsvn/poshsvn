param (
)

function RenderPage {
    param (
        $Content,
        $OutputPath,
        $Title
    )

    $template = Get-Content "$PSScriptRoot\..\www\template.html"
    $Content = $template -replace "{{content}}", $content -replace "{{title}}", $Title

    mkdir $OutputPath -Force
    Set-Content -Path "$OutputPath\index.html" -Value $Content -Force
}

if ($null -eq (Get-Module -ListAvailable -Name platyPS)) {
    Install-Module -Name platyPS -Force
}

Import-Module $PSScriptRoot\..\bin\poshsvn\PoshSvn.psd1

$outDir = "$PSScriptRoot\..\www\docs"
Remove-Item -Recurse -Force $outDir -ErrorAction SilentlyContinue

foreach ($path in Get-ChildItem "$PSScriptRoot\..\docs") {
    $null = $path -match "([a-zA-Z\-]*)\.md"
    $cmdletName = $Matches[1]
    RenderPage -Content (ConvertFrom-Markdown $path).Html -OutputPath "$outDir\$cmdletName" -Title $cmdletName
}
