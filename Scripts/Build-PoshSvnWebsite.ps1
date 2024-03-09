param (
)

$siteRoot = "$PSScriptRoot\..\www"
$outDir = "$siteRoot\build"

function RenderPage {
    param (
        $Content,
        $PageName,
        $Title
    )

    $template = Get-Content "$PSScriptRoot\..\www\template.html"
    $Content = $template -replace "{{content}}", $content -replace "{{title}}", $Title

    mkdir "$outDir\$PageName" -Force
    Set-Content -Path "$outDir\$PageName\index.html" -Value $Content -Force
}

if ($null -eq (Get-Module -ListAvailable -Name platyPS)) {
    Install-Module -Name platyPS -Force
}

Remove-Item -Recurse -Force $outDir -ErrorAction SilentlyContinue

foreach ($path in Get-ChildItem "$PSScriptRoot\..\docs") {
    $null = $path -match "([a-zA-Z\-]*)\.md"
    $cmdletName = $Matches[1]
    RenderPage -Content (ConvertFrom-Markdown $path).Html -PageName "docs\$cmdletName" -Title $cmdletName
}

Copy-Item "$siteRoot\static\*" $outDir -Recurse

foreach ($path in Get-ChildItem "$siteRoot\pages") {
    $content = Get-Content $path
    
    if ($path.Name -eq "index.html") {
        $pageName = ""
    }
    else {
        $pageName = $path.BaseName
    }

    RenderPage -Content $content -PageName $pageName -Title $path.BaseName
}
