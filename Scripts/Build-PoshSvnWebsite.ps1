param (
)

$siteRoot = "$PSScriptRoot\..\www"
$outDir = "$siteRoot\build"

$sitemap = "$outDir\sitemap.xml"

function Add-PageToSiteMap {
    param (
        [string]
        $PagePath
    )

    $url = ($PagePath | Resolve-Path -Relative -RelativeBasePath $outDir)
    $url = $url -replace "\.\\"
    $url = "https://www.poshsvn.com/$url/"
    $url = $url -replace "\\", "/" -replace "/index.html"

    $newNode = 
"  <url>
    <loc>$url</loc>
    <changefreq>always</changefreq>
    <priority>1.0</priority>
  </url>"

    Add-Content -Path $sitemap -Value $newNode
}

function TrimName {
    param (
        $Name
    )

    $null = $Name -match "[0-9]* *(.*)";
    return $Matches[1];
}

function RenderPage {
    param (
        $Content,
        $PageName,
        $Title
    )

    $topics = ""

    foreach ($folder in Get-ChildItem "$PSScriptRoot\..\PoshSvn.Docs\en-US" -Directory -Exclude "obj") {
        $topics += "<li class='mt-4'><h6>$(TrimName -Name $folder.Name)</h6></li><li><hr class='sidebar-divider'></li>"

        foreach ($path in $folder | Get-ChildItem -Filter "*.md" -File -Recurse) {
            $trimmedName = TrimName -Name $path.BaseName

            if ($Title -eq $trimmedName) {
                $active = "active"
            }
            else {
                $active = ""
            }
    
            $topics += "<li class='nav-item'><a class='nav-link $active' href='../$trimmedName/'>$trimmedName</a></li>"
        }
    }

    $template = Get-Content "$PSScriptRoot\..\www\template.html"
    $Content = $template -replace "{{content}}", $Content
    $Content = $Content -replace "{{title}}", $Title
    $Content = $Content -replace "{{topics}}", $topics
    $Content = $Content -replace "{{description}}", "$Title PoshSvn | Apache Subversion client for PowerShell."
    $Content = $Content -replace "<h1", '<h1 class="h3"'
    $Content = $Content -replace "<h2", '<h2 class="h4"'
    $Content = $Content -replace "<h3", '<h2 class="h5"'

    mkdir "$outDir\$PageName" -Force
    Set-Content -Path "$outDir\$PageName\index.html" -Value $Content -Force

    Add-PageToSiteMap -PagePath "$outDir\$PageName"
}

if ($null -eq (Get-Module -ListAvailable -Name platyPS)) {
    Install-Module -Name platyPS -Force
}

Remove-Item -Recurse -Force $outDir -ErrorAction SilentlyContinue

New-Item -ItemType File -Path $sitemap -Force
Add-Content -Path $sitemap -Value '<?xml version="1.0" encoding="utf-8"?>'
Add-Content -Path $sitemap -Value '<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">'

function RenderDocsLanguage {
    param (
        $SourceDir,
        $DestinationPrefix
    )

    foreach ($path in Get-ChildItem -Path "$SourceDir" -Filter "*.md" -File -Recurse) {
        $content = (ConvertFrom-Markdown $path).Html
        $trimmedName = TrimName -Name $path.BaseName
        RenderPage -Content $content -PageName "$DestinationPrefix\docs\$trimmedName" -Title $trimmedName
    }
}

RenderDocsLanguage -SourceDir "$PSScriptRoot\..\PoshSvn.Docs\en-US" -DestinationPrefix ""
# RenderDocsLanguage -SourceDir "$PSScriptRoot\..\PoshSvn.Docs\fr-FR" -DestinationPrefix "fr"

Copy-Item "$siteRoot\static\*" $outDir -Recurse

Copy-Item "$PSScriptRoot\..\Assets\icon-minimal.svg" "$outDir\favicon.svg"
Copy-Item "$PSScriptRoot\..\Assets\icon.svg" "$outDir\icon.svg"

Add-PageToSiteMap -PagePath "$outDir\index.html"

Add-Content -Path $sitemap -Value "</urlset>"
