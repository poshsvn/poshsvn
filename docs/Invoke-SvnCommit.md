---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
schema: 2.0.0
---

# Invoke-SvnCommit

## SYNOPSIS
Send changes from your working copy to the repository.

## SYNTAX

```
Invoke-SvnCommit [[-Path] <String[]>] -Message <String> [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

## DESCRIPTION
Send changes from your working copy to the repository.

svn-commit will send any lock tokens that it finds and will release locks on all PATHs committed (recursively) unless -NoUnlock is passed (TODO:).

## EXAMPLES

### Example 1
```powershell
PS C:\> svn-commit -Message "added howto section."

M        a.txt
Committed revision 3.
```

Commit a simple modification to a file with the commit message on the command line and an implicit target of your current directory ("`.`"):

### Example 2
```powershell
PS C:\> svn-commit -Message "removed file 'c'."

D        c.txt
Committed revision 7.
```

This command will commit a file scheduled for deletion.

## PARAMETERS

### -Message
Indicates that you will specify either a log message or a lock comment on the command line, following this option. For example: `svn-commit -Message "They don't make Sunday."`

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Specifies a Path of a the item commit. By default is `.` (current directory).

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
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

### PoshSvn.CmdLets.SvnCommitOutput

## NOTES

## RELATED LINKS
