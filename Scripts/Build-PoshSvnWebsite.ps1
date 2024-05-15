param (
)

$siteRoot = "$PSScriptRoot\..\www"
$outDir = "$siteRoot\build"

function Add-PageToSiteMap {
    param (
        [string]
        $PagePath
    )

    $url = ($PagePath | Resolve-Path -Relative -RelativeBasePath $outDir)
    $url = $url -replace "\.\\"
    $url = "https://www.poshsvn.com/$url/"
    $url = $url -replace "\\", "/" -replace "/index.html"
    Add-Content -Path "$outDir\sitemap.txt" -Value $url
}

function RenderPage {
    param (
        $Content,
        $PageName,
        $Title
    )

    $topics = ""

    foreach ($folder in Get-ChildItem "$PSScriptRoot\..\PoshSvn.Docs\en-US" -Directory -Exclude "obj") {
        $null = $folder.Name -match "[0-9]* *(.*)"
        $folderName = $Matches[1]

        $topics += "<li class='mt-4'><h6>$folderName</h6></li><li><hr class='sidebar-divider'></li>"

        foreach ($path in $folder | Get-ChildItem -Filter "*.md" -File -Recurse) {
            if ($Title -eq $path.BaseName) {
                $active = "active"
            }
            else {
                $active = ""
            }
    
            $topics += "<li class='nav-item'><a class='nav-link $active' href='../$($path.BaseName)'>$($path.BaseName)</a></li>"
        }
    }

    $template = Get-Content "$PSScriptRoot\..\www\template.html"
    $Content = $template -replace "{{content}}", $Content
    $Content = $Content -replace "{{title}}", $Title
    $Content = $Content -replace "{{topics}}", $topics
    $Content = $Content -replace "{{description}}", $Title
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

function RenderDocsLanguage {
    param (
        $SourceDir,
        $DestinationPrefix
    )

    foreach ($path in Get-ChildItem -Path "$SourceDir" -Filter "*.md" -File -Recurse) {
        $content = (ConvertFrom-Markdown $path).Html
        RenderPage -Content $content -PageName "$DestinationPrefix\docs\$($path.BaseName)" -Title $path.BaseName
    }
}

RenderDocsLanguage -SourceDir "$PSScriptRoot\..\PoshSvn.Docs\en-US" -DestinationPrefix ""
# RenderDocsLanguage -SourceDir "$PSScriptRoot\..\PoshSvn.Docs\fr-FR" -DestinationPrefix "fr"

Copy-Item "$siteRoot\static\*" $outDir -Recurse

Copy-Item "$PSScriptRoot\..\Assets\icon-minimal.svg" "$outDir\favicon.svg"
Copy-Item "$PSScriptRoot\..\Assets\icon.svg" "$outDir\icon.svg"

Add-PageToSiteMap -PagePath "$outDir\index.html"
