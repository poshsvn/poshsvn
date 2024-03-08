function Build-PoshSvn {
    param (
        [string]$Configuration = "Release"
    )

    $Output = "$PSScriptRoot\bin\poshsvn"
    $ProjectPath = "$PSScriptRoot\PoshSvn\PoshSvn.csproj"

    Remove-Item $Output -Recurse -Force -ErrorAction Ignore

    dotnet.exe build $ProjectPath --output $Output --configuration $Configuration -v=normal

    Compress-Archive -Path $Output\* -DestinationPath "$Output.zip" -Force -CompressionLevel Optimal
}

function Test-PoshSvn {
    param (
    )

    dotnet.exe test $PSScriptRoot --configuration Release --settings Default.runsettings -v=normal
}

function Publish-PoshSvn {
    param (
        [string]$NuGetApiKey
    )
    
    Publish-Module -Path "$PSScriptRoot\bin\poshsvn" -NuGetApiKey $NuGetApiKey
}

function Build-PoshSvnWebsite {
    param (
    )

    Install-Module -Name platyPS -ErrorAction SilentlyContinue

    Import-Module $PSScriptRoot\bin\poshsvn\PoshSvn.psd1

    $outDir = "$PSScriptRoot\www\docs"
    Remove-Item -Recurse -Force $outDir -ErrorAction SilentlyContinue

    $paths = New-MarkdownHelp -Module PoshSvn -OutputFolder $outDir -Force -NoMetadata

    foreach ($path in $paths) {
        $null = $path -match "([a-zA-Z\-]*)\.md"
        RenderPage -Content (ConvertFrom-Markdown $path).Html -OutputPath "$($path -replace ".md")" -Title $Matches[1]
    }
}

function RenderPage {
    param (
        $Content,
        $OutputPath,
        $Title
    )

    $template = Get-Content "$PSScriptRoot\www\template.html"
    $Content = $template -replace "{{content}}", $content -replace "{{title}}", $Title

    mkdir $OutputPath -Force
    Set-Content -Path "$OutputPath\index.html" -Value $Content -Force
}
