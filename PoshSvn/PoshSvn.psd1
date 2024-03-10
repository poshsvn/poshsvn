@{
    GUID                   = "{49B991B6-D257-4122-AEDA-0C317118596A}"
    Author                 = "Timofei Zhakov"
    CompanyName            = "Rinrab"
    Copyright              = "(c) Timofei Zhakov. All rights reserved."
    ModuleVersion          = "0.1.2"
    PowerShellVersion      = "3.0"
    CLRVersion             = "4.0"
    Description            = "Apache Subversion client for PowerShell

Project website and documentation: https://www.poshsvn.com"
    ProcessorArchitecture  = "Amd64"
    RootModule             = "PoshSvn.dll"
    DotNetFrameworkVersion = "4.7.2"
    CmdletsToExport        = @(
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
        "Invoke-SvnCopy"
    )
    AliasesToExport        = @(
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
        "svn-copy"
        "svnadmin-create"
    )
    FunctionsToExport      = @()
    FormatsToProcess       = @(
        "SvnStatus.format.ps1xml"
        "SvnInfo.format.ps1xml"
        "SvnNotifyOutput.format.ps1xml"
        "SvnLog.format.ps1xml"
        "SvnItem.format.ps1xml"
    )
    FileList               = @(
        # PoshSvn
        "PoshSvn.dll"
        "PoshSvn.psd1"

        ## Formats
        "SvnInfo.format.ps1xml"
        "SvnItem.format.ps1xml"
        "SvnLog.format.ps1xml"
        "SvnNotifyOutput.format.ps1xml"
        "SvnStatus.format.ps1xml"

        # SharpSvn
        "SharpSvn.dll"
        "SharpSvn-DB44-20-win32.svnDll"
        "SharpPlink-Win32.svnExe"

        ## CRT
        "vcruntime140.dll"
        "vcruntime140_1.dll"
    )
    PrivateData            = @{
        PSData = @{
            Tags                     = @("svn", "subversion")
            Prerelease               = "alpha"
            ReleaseNotes             = "Changelog will be available soon."
            # LicenseUri = "https://aka.ms/azps-license"
            ProjectUri = "https://www.poshsvn.com"
            # IconUri = ""
            RequireLicenseAcceptance = $false
            # ExternalModuleDependencies = @()
        }
    }
}
