---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnMkdir/
schema: 2.0.0
---

# Invoke-SvnMkdir

## SYNOPSIS
Create a new directory under version control.

## SYNTAX

### Local (Default)
```
Invoke-SvnMkdir [-Target] <SvnTarget[]> [-Parents] [<CommonParameters>]
```

### Remote
```
Invoke-SvnMkdir [-Target] <SvnTarget[]> [-Message <String>] [-Parents] [<CommonParameters>]
```

## DESCRIPTION
Create version controlled directories.

1. Each directory specified by a working copy PATH is created locally
   and scheduled for addition upon the next commit.
2. Each directory specified by a URL is created in the repository via
   an immediate commit.

In both cases, all the intermediate directories must already exist,
unless the -Parents option is given.

## EXAMPLES

### Example 1
```powershell
{{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Message
{{ Fill Message Description }}

```yaml
Type: String
Parameter Sets: Remote
Aliases: m

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Parents
{{ Fill Parents Description }}

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

### -Target
{{ Fill Target Description }}

```yaml
Type: SvnTarget[]
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
