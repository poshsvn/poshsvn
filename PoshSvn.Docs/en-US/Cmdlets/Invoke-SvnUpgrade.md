---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnUpgrade
schema: 2.0.0
---

# Invoke-SvnUpgrade

## SYNOPSIS
Upgrade the metadata storage format for a working copy.

## SYNTAX

```
Invoke-SvnUpgrade [[-Path] <String[]>] [<CommonParameters>]
```

## DESCRIPTION
Upgrade the metadata storage format for a working copy.
Local modifications are preserved.

## EXAMPLES

### Example 1
```powershell
svn-upgrade path/to/wc
Upgraded '.'
```

Upgrade the working copy, located at 'path/to/wc'.

## PARAMETERS

### -Path
Specifies the Path of a working copy to upgrade. By default is `.` (current directory).

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### PoshSvn.SvnNotifyOutput

## NOTES

## RELATED LINKS
