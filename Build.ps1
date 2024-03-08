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
    Update-MarkdownHelp -Path $PSScriptRoot\docs
}
