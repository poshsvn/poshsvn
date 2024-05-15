---
description: Describes basics of PoshSvn usage.
Locale: en-US
online version: https://www.poshsvn.com/docs/about_PoshSvn/
schema: 2.0.0
title: About PoshSvn
---

# about_PoshSvn

## Short description

Describes basics of PoshSvn usage.

## Long description

PoshSvn provides the following features:

- Tab tab-completion (tab-expansion) for commands and parameters.
- Typed output of cmdlets.
- Formatted output to repeat the Subversion command line interface user experience.

## Concept

The PoshSvn concept was to repeat the Subversion command line interface
user experience, including parameters, output, and other behavior.

## Examples

### Example 1

The following command checks out the repository using Subverison CLI:

```shell
svn checkout https://svn.apache.org/repos/asf/serf/trunk serf-trunk
A    serf-trunk\test
A    serf-trunk\test\MockHTTPinC
A    serf-trunk\test\certs
A    serf-trunk\test\certs\private
A    serf-trunk\test\certs\serfserver_san_nocn_cert.pem
...
 U   serf-trunk
Checked out revision 1916201.
```

While PoshSvn requires:

```powershell
svn-checkout https://svn.apache.org/repos/asf/serf/trunk serf-trunk

A       serf-trunk\test
A       serf-trunk\test\MockHTTPinC
A       serf-trunk\test\certs
A       serf-trunk\test\certs\private
A       serf-trunk\test\certs\serfserver_san_nocn_cert.pem
...
U       serf-trunk
Checked out revision 1916201.
```

Also you can write the names of the parameters (better for scripting):

```powershell
svn-checkout -Url https://svn.apache.org/repos/asf/serf/trunk -Path serf-trunk
```

As you can see, there are very little differences between Subversion
CLI and PoshSvn cmdlets, but PoshSvn also adds some PowerShell
features such as progress, typed output, and other.
