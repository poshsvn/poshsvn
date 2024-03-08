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

function Update-Docs {
    param (
    )

    Build-PoshSvn
    Import-Module $PSScriptRoot\bin\poshsvn\PoshSvn.psd1 -Force
    Update-MarkdownHelp -Path $PSScriptRoot\docs -ExcludeDontShow -AlphabeticParamsOrder
}

function Build-PoshSvnWebsite {
    param (
    )

    if ($null -eq (Get-Module -ListAvailable -Name platyPS)){
        Install-Module -Name platyPS -Force
    }

    Import-Module $PSScriptRoot\bin\poshsvn\PoshSvn.psd1

    $outDir = "$PSScriptRoot\www\docs"
    Remove-Item -Recurse -Force $outDir -ErrorAction SilentlyContinue

    foreach ($path in Get-ChildItem "$PSScriptRoot\docs") {
        $null = $path -match "([a-zA-Z\-]*)\.md"
        $cmdletName = $Matches[1]
        RenderPage -Content (ConvertFrom-Markdown $path).Html -OutputPath "$outDir\$cmdletName" -Title $cmdletName
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
