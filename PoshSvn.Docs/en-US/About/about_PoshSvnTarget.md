---
description: Describes how to specify cmdlet target.
Locale: en-US
online version: https://www.poshsvn.com/docs/about_PoshSvnTarget/
schema: 2.0.0
title: About PoshSvn Cmdlet Target
---

# PoshSvn
## about_PoshSvnTarget

## Short description
Describes how to specify cmdlet target.

## Long description

Some Subversion operations support specifying either a local Path to a working copy or a remote Url to a repository as a target.

You can specify target of the operation by setting the
`-Target` parameter to either Path or Url. However,
if you prefer to directly indicate whether it's a URL
or a Path, the New-SvnTarget cmdlet and specify target
via `-Url` or `-Path` parameters respectively.

### Specifing the target via `-Target` Parameter

This method of specifying the target is optimal for basic
usage of Subversion. You can pass either a Path or Url to
the `-Target` parameter, which PoshSvn will interpret accordingly.

The following examples retrieve `svn-info` from the remote repository and the working copy:

```powershell
svn-info -Target https://svn.apache.org/repos/asf/serf/trunk
svn-info -Target .\path\to\wc
```

However, you don't need to write `-Target` before specifing,
as it accepts value from remaining arguments. That's why it
almost repeat the Subversion CLI behaivior:

```powershell
svn-info https://svn.apache.org/repos/asf/serf/trunk
svn-info .\path\to\wc
```

In some cmdlets, if no target is specified, PoshSvn will
default to the current directory. This means that the
following example will retrieve `svn-info` about the
working copy located at `C:\path\to\wc`:

```
cd C:\path\to\wc
svn-info
```

### Specifing the target via directly indicating its type

If you write a script, you can explicitly specify whether
the target is a Url or a Path. This approach is optimal
for scripts as it ensures readability, understandability,
and helps prevent mistakes caused by incorrect detection of the target type:

```powershell
svn-info (New-SvnTarget -Url https://svn.apache.org/repos/asf/serf/trunk)
svn-info (New-SvnTarget -Path .\path\to\wc)
```

If you prefer, you may use a variable for the cmdlet target. For exmaple:

```powershell
# Create target
$target = (New-SvnTarget -Url https://svn.apache.org/repos/asf/serf/trunk)

# Print target
Write-Output $target

# Invoke svn-info
svn-info -Target $target
```

If a target with a different type is specified, it will result in an error:

```powershell
svn-info (New-SvnTarget -Path https://svn.apache.org/repos/asf/serf/trunk)
# Error
svn-info (New-SvnTarget -Url .\path\to\wc)
# Error
```

Additionally, you may utilize the
[`$PSScriptRoot`](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_automatic_variables?view=powershell-7.4#psscriptroot)
variable, if your working copy is located near the script.
This automatic variable provides the path to the directory
containing the script.

```powershell
svn-info (New-SvnTarget -Path $PSScriptRoot\path\to\wc)
```
