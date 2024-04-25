@{
    GUID                   = "{49B991B6-D257-4122-AEDA-0C317118596A}"
    Author                 = "Timofei Zhakov"
    CompanyName            = "Rinrab"
    Copyright              = "(c) Timofei Zhakov. All rights reserved."
    ModuleVersion          = "@Version@"
    PowerShellVersion      = "3.0"
    CLRVersion             = "4.0"
    Description            = "Apache Subversion client for PowerShell

Project website: https://www.poshsvn.com"
    ProcessorArchitecture  = "@ProcessorArchitecture@"
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
        "Invoke-SvnSwitch"
        "Invoke-SvnExport"
        "Invoke-SvnImport"
        "Invoke-SvnCat"
        "Invoke-SvnDiff"
        "Invoke-SvnBlame"
        "Invoke-SvnLock"
        "Invoke-SvnUnlock"
        "Invoke-SvnMerge"
        "New-SvnTarget"
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
        "svn-switch"
        "svn-export"
        "svn-import"
        "svn-cat"
        "svn-diff"
        "svn-blame"
        "svn-lock"
        "svn-unlock"
        "svn-merge"
        "svnadmin-create"
    )
    FunctionsToExport      = @()
    FormatsToProcess       = @(
        "SvnStatus.format.ps1xml"
        "SvnInfo.format.ps1xml"
        "SvnNotifyOutput.format.ps1xml"
        "SvnLog.format.ps1xml"
        "SvnItem.format.ps1xml"
        "SvnBlame.format.ps1xml"
    )
    PrivateData            = @{
        PSData = @{
            Tags                     = @("svn", "subversion")
            ReleaseNotes             = "@ReleaseNotes@"
            # LicenseUri = "https://aka.ms/azps-license"
            ProjectUri = "https://www.poshsvn.com"
            IconUri = "https://www.poshsvn.com/icon.svg"
            RequireLicenseAcceptance = $false
            # ExternalModuleDependencies = @()
        }
    }
}
