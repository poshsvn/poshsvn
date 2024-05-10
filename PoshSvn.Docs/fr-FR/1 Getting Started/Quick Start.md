# Quick Start with PoshSvn

## 1. Install PoshSvn

The easiest method for installing the PoshSvn module is through
the PowerShell Gallery. You can install PoshSvn using the following command:

```powershell
Install-Module -Name PoshSvn
```

## 2. Create a repository

To begin using PoshSvn, you need to create an empty repository.

Repository can be created via `svnadmin-create` cmdlet:

```powershell
# You can choose any other location of the repository.
cd C:\Repositories

svnadmin-create -Path repos
```

## 3. Import data to your repository

```powershell
$sourcePath = "C:\Users\sally\Documents\myproject"
$destinationUrl = "file:///C:/Repositories/repos/myproject"

svn-import -Source $sourcePath -Destination $destinationUrl -m "initial import"
A       C:\Users\sally\Documents\myproject\foo.c
A       C:\Users\sally\Documents\myproject\bar.c
A       C:\Users\sally\Documents\myproject\Project1.vcproj
A       C:\Users\sally\Documents\myproject\Project1.sln
...
Committed revision 1.
```

## 4. Checkout your repository

```powershell
svn-checkout file:///C:/Repositories/repos/myproject myproject
```

## 5. Commit your changes

To commit your changes, you can use the `svn-commit` cmdlet, as shown bellow.

```powershell
svn-commit -m "My Descriptive Log Message"
M       C:\Users\sally\Documents\myproject\foo.c
M       C:\Users\sally\Documents\myproject\bar.c
```
