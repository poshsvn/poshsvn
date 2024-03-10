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

    $topics = ""
    foreach ($path in Get-ChildItem "$PSScriptRoot\..\docs") {
        if ($Title -eq $path.BaseName) {
            $active = "active"
        }
        else {
            $active = ""
        }

        $topics += "<li class=`"nav-item`"><a class=`"nav-link $active`" href='/docs/$($path.BaseName)'>$($path.BaseName)</a></li>"
    }

    $template = Get-Content "$PSScriptRoot\..\www\template.html"
    $Content = $template -replace "{{content}}", $content -replace "{{title}}", $Title -replace "{{topics}}", $topics

    mkdir "$outDir\$PageName" -Force
    Set-Content -Path "$outDir\$PageName\index.html" -Value $Content -Force
}

if ($null -eq (Get-Module -ListAvailable -Name platyPS)) {
    Install-Module -Name platyPS -Force
}

Remove-Item -Recurse -Force $outDir -ErrorAction SilentlyContinue

foreach ($path in Get-ChildItem "$PSScriptRoot\..\docs") {
    $null = $path -match "([a-zA-Z\-_]*)\.md"
    $cmdletName = $Matches[1]
    RenderPage -Content (ConvertFrom-Markdown $path).Html -PageName "docs\$cmdletName" -Title $cmdletName
}

Copy-Item "$siteRoot\static\*" $outDir -Recurse

foreach ($path in Get-ChildItem "$siteRoot\pages" -ErrorAction SilentlyContinue) {
    $content = Get-Content $path
    
    if ($path.Name -eq "index.html") {
        $pageName = ""
    }
    else {
        $pageName = $path.BaseName
    }

    RenderPage -Content $content -PageName $pageName -Title $path.BaseName
}
