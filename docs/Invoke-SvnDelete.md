---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnDelete/
schema: 2.0.0
---

# Invoke-SvnDelete

## SYNOPSIS
Remove files and directories from version control.

## SYNTAX

### Target
```
Invoke-SvnDelete [-Target] <String[]> [-Message <String>] [-Force] [-KeepLocal]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### Path
```
Invoke-SvnDelete -Path <String[]> [-Force] [-KeepLocal] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### Url
```
Invoke-SvnDelete -Url <Uri[]> -Message <String> [-Force] [-KeepLocal] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

## DESCRIPTION
Removes local or remote items from version.

Each item specified by a Path is scheduled for deletion upon the next commit.

Each item specified by a Url is deleted from the repository via an immediate commit.

## EXAMPLES

### Example 1
```powershell
svn-delete foo.c

D       foo.c
```

Removes and schedules the foo.c file for deletion.

### Example 1
```powershell
svn-delete https://svn.example.com/repos/foo.c -Message "delete foo.c"

Committed revision 57.
```

This command removes the file located at `https://svn.example.com/repos/foo.c`
and commits the deletion with the log message "delete foo.c".

## PARAMETERS

### -Force
Force operation to run

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: f

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -KeepLocal
Keep item in working copy.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: keep-local

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Message
Specifies the log message.

```yaml
Type: String
Parameter Sets: Target
Aliases: m

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: Url
Aliases: m

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Specifies the Path of an item in a working copy to delete.

```yaml
Type: String[]
Parameter Sets: Path
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Target
Specifies the Path or the Url of an item to delete.

```yaml
Type: String[]
Parameter Sets: Target
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Url
Specifies the Url of an item in a repository to delete.

```yaml
Type: Uri[]
Parameter Sets: Url
Aliases:

Required: True
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

### PoshSvn.CmdLets.SvnCommitOutput

## NOTES

## RELATED LINKS
