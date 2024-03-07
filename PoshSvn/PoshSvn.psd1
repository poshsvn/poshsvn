@{
    GUID="{49B991B6-D257-4122-AEDA-0C317118596A}"
    Author="Timofei Zhakov"
    CompanyName = 'Rinrab'
    Copyright="(c) Timofei Zhakov. All rights reserved."
    ModuleVersion="0.1.0.0"
    PowerShellVersion="3.0"
    CLRVersion="4.0"
    Description = 'Subversion client for PowerShell'
    ProcessorArchitecture = 'Amd64'
    RootModule = "PoshSvn.dll"
    DotNetFrameworkVersion = '4.7.2'
    PrivateData = @{
        PSData = @{
            Tags = @("svn", "subversion")
            Prerelease = 'alpha'
            ReleaseNotes = "Initial release."
            # LicenseUri = 'https://aka.ms/azps-license'
            # ProjectUri = 'https://github.com/Azure/azure-powershell'
            # IconUri = ''
            RequireLicenseAcceptance = $false
            # ExternalModuleDependencies = @()
        }
    }
    CmdletsToExport= @(
        "Invoke-SvnStatus"
        "Invoke-SvnUpdate"
        "Invoke-SvnCheckout"
        "Invoke-SvnCleanup"
        "Invoke-SvnCommit"
        "Invoke-SvnMkdir"
        "Invoke-SvnInfo"
        "Invoke-SvnAdd"
        "Invoke-SvnDelete"
        "Invoke-SvnAdminCreate"
        "Invoke-SvnMove"
        "Invoke-SvnLog"
        "Invoke-SvnList"
        "Invoke-SvnRevert"
    )
    AliasesToExport = @(
        "svn-status"
        "svn-update"
        "svn-checkout"
        "svn-cleanup"
        "svn-commit"
        "svn-mkdir"
        "svn-info"
        "svn-add"
        "svn-delete"
        "svn-remove"
        "svn-move"
        "svn-log"
        "svn-list"
        "svn-revert"
        "svnadmin-create"
    )
    FunctionsToExport = @()
    FormatsToProcess = @(
        "SvnStatus.format.ps1xml"
        "SvnInfo.format.ps1xml"
        "SvnNotifyOutput.format.ps1xml"
        "SvnLog.format.ps1xml"
        "SvnItem.format.ps1xml"
    )
}
