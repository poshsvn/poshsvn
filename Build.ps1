function Build-PoshSvn {
    param (
        [string]$Configuration = "Release"
    )

    $Output = "$PSScriptRoot\publish"
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
