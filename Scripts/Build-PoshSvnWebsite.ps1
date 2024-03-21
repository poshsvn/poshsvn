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

    $lastPrefix = ""
    $topics = ""
    foreach ($path in Get-ChildItem "$PSScriptRoot\..\docs") {
        $null = $path.BaseName -match "(^.*[-_])"
        $prefix = $Matches[1]
        $prefix = $prefix -replace "[a-zA-Z]*-", "Cmdlets"
        $prefix = $prefix -replace "about_", "About"

        if ($lastPrefix -ne $prefix) {
            $lastPrefix = $prefix
            $topics += "<li class='mt-4'><h6>$($prefix)</h6></li><li><hr class='sidebar-divider'></li>"
        }

        if ($Title -eq $path.BaseName) {
            $active = "active"
        }
        else {
            $active = ""
        }

        $topics += "<li class='nav-item'><a class='nav-link $active' href='/docs/$($path.BaseName)'>$($path.BaseName)</a></li>"
    }

    $template = Get-Content "$PSScriptRoot\..\www\template.html"
    $Content = $template -replace "{{content}}", $Content
    $Content = $Content -replace "{{title}}", $Title
    $Content = $Content -replace "{{topics}}", $topics
    $Content = $Content -replace "<h1", '<h1 class="h3"'
    $Content = $Content -replace "<h2", '<h2 class="h4"'
    $Content = $Content -replace "<h3", '<h2 class="h5"'

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
    $content = (ConvertFrom-Markdown $path).Html -replace '<h1 id="poshsvn">PoshSvn</h1>'
    RenderPage -Content $content -PageName "docs\$cmdletName" -Title $cmdletName
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

New-Item "$outDir\sitemap.txt"

foreach ($path in Get-ChildItem $outDir -Recurse -Filter "*.html" -Exclude "google*") {
    $url = ($path | Resolve-Path -Relative -RelativeBasePath $outDir)
    $url = $url -replace "\.\\"
    $url = "https://www.poshsvn.com/$url"
    $url = $url -replace "\\", "/" -replace "/index.html"
    Add-Content -Path "$outDir\sitemap.txt" -Value $url
}

Copy-Item "$PSScriptRoot\..\icon-minimal.svg" "$outDir\favicon.svg"
Copy-Item "$PSScriptRoot\..\icon.svg" "$outDir\icon.svg"
