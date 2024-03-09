# Invoke-SvnAdd

## SYNOPSIS
Add files, directories, or symbolic links.

## SYNTAX

```
Invoke-SvnAdd [-Path] <String[]> [-Depth <SvnDepth>] [-Force] [-NoIgnore] [-NoAutoProps] [-Parents]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
Schedule files, directories, or symbolic links in your working copy for addition to the repository. They will be uploaded and added to the repository on your next commit. If you add something and change your mind before committing, you can unschedule the addition using `svn-revert`.

## EXAMPLES

### Example 1
```powershell
PS C:\> svn-add .\foo.c

A       foo.c
```

Adds a file from your working copy.

### Example 2
```powershell
PS C:\> svn-add testdir

A       testdir
A       testdir\a
A       testdir\b
A       testdir\c
A       testdir\d
```

When adding a directory, the default behavior of svn-add is to recurse.

### Example 3
```powershell
PS C:\> svn-add otherdir -Depth Empty

A       otherdir
```

You can add a directory without adding its contents.

# Example 4
TODO:

## PARAMETERS

### -Depth
Instructs Subversion to limit the scope of an operation to a particular tree depth. ARG is one of empty (only the target itself), files (the target and any immediate file children thereof), immediates (the target and any immediate children thereof), or infinity (the target and all of its descendants—full recursion). By default is recursive.

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
Forces a particular command or operation to run. Subversion will prevent you from performing some operations in normal usage, but you can pass this option to tell Subversion "I know what I'm doing as well as the possible repercussions of doing it, so let me at 'em." This option is the programmatic equivalent of doing your own electrical work with the power on-if you don't know what you're doing, you're likely to get a nasty shock.

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
Shows files in the status listing or adds/imports files that would normally be omitted since they match a pattern in the `global-ignores` configuration option or the `svn:ignore` or `svn:global-ignoresproperties`.

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
Creates and adds nonexistent or nonversioned parent directories to the working copy or repository as part of an operation. This is useful for automatically creating multiple subdirectories where none currently exist. If performed on a URL, all the directories will be created in a single commit.

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
Specifies a Path of a the item to add.

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

### -ProgressAction
{{ Fill ProgressAction Description }}

```yaml
Type: ActionPreference
Parameter Sets: (All)
Aliases: proga

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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
