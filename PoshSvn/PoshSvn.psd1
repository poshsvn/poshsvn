@{
    GUID="{49B991B6-D257-4122-AEDA-0C317118596A}"
    Author="Timofei Zhakov"
    Copyright="(c) Timofei Zhakov. All rights reserved."
    ModuleVersion="0.1.0.0"
    PowerShellVersion="3.0"
    CLRVersion="4.0"
    RootModule = "PoshSvn.dll"
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
        "Add-SvnItem"
        "svn-delete"
        "svn-remove"
        "Remove-SvnItem"
        "svn-move"
        "Move-SvnItem"
        "svnadmin-create"
    )
    FunctionsToExport = @()
    FormatsToProcess = @(
        "SvnStatus.format.ps1xml"
        "SvnUpdate.format.ps1xml"
        "SvnCheckout.format.ps1xml"
        "SvnCommit.format.ps1xml"
        "SvnInfo.format.ps1xml"
        "SvnNotifyOutput.format.ps1xml"
    )
}
