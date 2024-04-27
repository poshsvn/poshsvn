---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnMkdir/
schema: 2.0.0
---

# Invoke-SvnMkdir

## SYNOPSIS
{{ Fill in the Synopsis }}

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
{{ Fill in the Description }}

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
