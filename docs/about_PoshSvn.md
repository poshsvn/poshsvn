# PoshSvn
## about_PoshSvn

# SHORT DESCRIPTION

Apache Subversion for PowerShell.

# LONG DESCRIPTION

PoshSvn provides the following features:

- Tab tab-completion (tab-expansion) for commands and parameters.
- Typed cmdlet output.
- Formatted to repeat the Subversion command line interface user experience.

# Concept

The PoshSvn concept was to repeat the Subversion command line interface user experience, including parameters, output, and other behavior.

## Examples

For better clarity, Subversion CLI examples will be indicated with `$`, while PoshSvn examples will be indicated with `PS C:\>`.

### Example 1

The following command checks out the repository using Subverison CLI:

```shell
$ svn checkout https://svn.apache.org/repos/asf/serf/trunk serf-trunk
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
PS C:\> svn-checkout https://svn.apache.org/repos/asf/serf/trunk serf-trunk

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
PS C:\> svn-checkout -Url https://svn.apache.org/repos/asf/serf/trunk -Path serf-trunk
```

As you can see, there are very little differences between Subversion CLI and PoshSvn cmdlets, but PoshSvn also adds some PowerShell features such as progress, typed output, and other. 
