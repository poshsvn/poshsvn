---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnAdd/
schema: 2.0.0
---

# Invoke-SvnAdd

## SYNOPSIS
Add files, directories, or symbolic links.

## SYNTAX

```
Invoke-SvnAdd [-Path] <String[]> [-Depth <SvnDepth>] [-Force] [-NoIgnore] [-NoAutoProps] [-Parents]
 [<CommonParameters>]
```

## DESCRIPTION
Schedule files, directories, or symbolic links in your
working copy for addition to the repository.
They will be uploaded and added to the repository on your next commit.

## EXAMPLES

### Example 1: Add a file

To add a file to your working copy:

```powershell
svn-add .\foo.c

A       foo.c
```

### Example 2: Add a directory with content

When adding a directory, the default behavior of `svn-add` is to recurse:

```powershell
svn-add testdir

A       testdir
A       testdir\a
A       testdir\b
A       testdir\c
A       testdir\d
```

### Example 3: Add a directory without content

You can add a directory without adding its contents:

```powershell
svn-add otherdir -Depth Empty

A       otherdir
```

### Example 4: Add versioned directory

Attempts to add an item which is already versioned will fail by default.
To override the default behavior and force Subversion to recurse into
already-versioned directories, pass the `-Force` option:

```powershell
svn-add VersionedDirictory
Invoke-SvnAdd: 'C:\Users\cmpilato\projects\subversion\site' is already under version control.
svn-add VersionedDirictory -Force
A        VersionedDirictory\foo.c
A        VersionedDirictory\somedir\bar.c
A        VersionedDirictory\otherdir\docs\baz.doc
```

## PARAMETERS

### -Depth
Limit the scope of the operation by specified depth (Empty, Files, Immediates, or Infinity).

```yaml
Type: SvnDepth
Parameter Sets: (All)
Aliases:
Accepted values: Empty, Files, Immediates, Infinity

Required: False
Position: Named
Default value: Recursive
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Ignore already versioned paths.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoAutoProps
Disables automatic property setting, overriding the enable-auto-props runtime configuration directive.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: no-auto-props

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoIgnore
Disregard default and svn:ignore and svn:global-ignores property ignores.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: no-ignore

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Parents
Add intermediate parents.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Specifies a Path of the item to add.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### PoshSvn.SvnNotifyOutput

## NOTES

## RELATED LINKS

[svn-commit](https://www.poshsvn.com/docs/Invoke-SvnCommit/)

[svn-delete](https://www.poshsvn.com/docs/Invoke-SvnDelete/)
