---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnAdminCreate/
schema: 2.0.0
---

# Invoke-SvnAdminCreate

## SYNOPSIS
Create a new, empty repository.

## SYNTAX

```
Invoke-SvnAdminCreate [-Path] <String[]> [-RepositoryType <RepositoryType>] [<CommonParameters>]
```

## DESCRIPTION
Create a new, empty repository at the path provided. If the provided directory does not exist, it will be created for you. As of Subversion 1.2, svnadmin creates new repositories with the FSFS filesystem backend by default.

While svnadmin create will create the base directory for a new repository, it will not create intermediate directories. For example, if you have an empty directory named `C:\svn`, creating `C:\svn\Repositories` will work, while attempting to create `C:\svn\subdirectory\Repositories` will fail with an error. Also, keep in mind that, depending on where on your system you are creating your repository, you might need to run svnadmin create as a user with elevated privileges (such as the administrator).

## EXAMPLES

### Example 1: Create an empty repository

Creating a new repository is this easy:

```powershell
cd C:\Repositories
svnadmin-create myrepo
```

This command will create an empty repository at `C:\Repositories\myrepo`.

### Example 2: Specifying repository type

In Subversion 1.0, a Berkeley DB repository is always created. In Subversion 1.1, a Berkeley DB repository is the default repository type, but an FSFS repository can be created using the `-RepositoryType` option:

```powershell
svnadmin-create C:\Repositories\myrepo -RepositoryType FsFs
```

## PARAMETERS

### -Path
Specifies the path of a repository to be created.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RepositoryType
When creating a repository, use ARG as the requested filesystem type. ARG may be either `BerkeleyDB` or `FsFs`.

```yaml
Type: RepositoryType
Parameter Sets: (All)
Aliases: fs-type, type, fs
Accepted values: FsFs

Required: False
Position: Named
Default value: FsFs
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
