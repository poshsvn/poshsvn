Import-Module "$PSScriptRoot\..\..\bin\Debug\PoshSvn.psd1"
Set-Location c:\
Remove-Item C:\Demo -Recurse -Force -Confirm
mkdir C:\Demo
Set-Location C:\Demo
mkdir repos
svnadmin-create repos
$PSStyle.Progress.View = 'Classic'
Clear-Host

