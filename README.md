# PoshSvn

Apache Subversion client for PowerShell.

## Features

- Tab tab-completion (tab-expansion) for commands and parameters.
- Typed output of cmdlets.
- Formatted output to repeat the Subversion command line interface user experience.

## Installation

### PowerShell Gallery

Use the following command to install PoshSvn from PowerShell Gallery:

```powershell
Install-Module -Name PoshSvn
```

### Compile yourself

#### Prerequities

To build PoshSvn you are required to have Visual Studio or Visual Studio Build
Tools installed on your machine with the following components:
- Desktop development with C++
  - vcpkg package manager
- .NET desktop development

#### Getting source code

You may use official github mirror of PoshSvn to obtain source code.
Use the following command to clone the repository:

```powershell
git clone https://github.com/rinrab/poshsvn poshsvn
cd .\poshsvn
```

The other way is to checkout PoshSvn from my subversion server by
link `https://svn.rinrab.com/rinrab/poshsvn/trunk`. You may use
the guest user with username `guest` and empty password.

```powershell
svn checkout https://svn.rinrab.com/rinrab/poshsvn/trunk poshsvn
# or if you magicaly installed poshsvn without installing it...
svn-checkout https://svn.rinrab.com/rinrab/poshsvn/trunk poshsvn

cd .\poshsvn
```

#### Build process

The only you need to build PoshSvn is to call `MSBuild` with and pass some parameters.

The following command builds PoshSvn with all its components:

```powershell
cd D:\Users\User\source\repos\opensource\rinrab\path\to\your\poshsvn\source\code\to\build
MSBuild.exe .\PoshSvn.sln /restore /property:Configuration=Release
```

This would take about 10 minutes to build all components. Next builds would take just a few seconds.

> [!IMPORTANT]
> Build from `Developer Command Prompt for VS` or `Developer PowerShell for VS`
> because the `MSBuild.exe` is required to be in PATH.

#### Install

To install PoshSvn you need to copy it to modules directory.

The PowerShell modules path contains in `PSModulePath` enviroment variable.
The default PowerShell modules path is `[Ducuments]\PowerShell\Modules`.

Here is script which installs PoshSvn module to first avalible `PSModulePath`.

```powershell
$modulesPath = $env:PSModulePath.Split(';')[0]
Copy-Item .\bin\poshsvn\ $modulesPath -Recurse -Force
```

To make sure that installation is done successfully, you may try to call
`svn-info` cmdlet as shown in the following example:

```powershell
PS C:\> svn-info https://svn.apache.org/repos/asf/serf/trunk

Path                  : trunk
URL                   : https://svn.apache.org/repos/asf/serf/trunk/
Relative URL          : serf/trunk/
Repository Root       : https://svn.apache.org/repos/asf/
Repository UUID       : 13f79535-47bb-0310-9956-ffa450edef68
...
```

#### Batch script

Maybe I will add a `build.bat` script, which does everything shown before.

### Via MSI installer

Not ready yet.

### From winget

Not ready yet.

```powershell
winget install rinrab.poshsvn
```

### Linux?

For now, PoshSvn doesn't support linux.
