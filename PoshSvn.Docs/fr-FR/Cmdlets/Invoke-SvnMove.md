---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnMove/
schema: 2.0.0
---

# Invoke-SvnMove

## SYNOPSIS
Moves an item from one location to another in a working copy or repository.

## SYNTAX

### Local (Default)
```
Invoke-SvnMove [-Source] <SvnTarget[]> [-Destination] <SvnTarget> [-Force] [-Parents] [-AllowMixedRevisions]
 [<CommonParameters>]
```

### Remote
```
Invoke-SvnMove [-Source] <SvnTarget[]> [-Destination] <SvnTarget> -Message <String> [-Force] [-Parents]
 [-AllowMixedRevisions] [<CommonParameters>]
```

## DESCRIPTION
Source and Destination can each be either a working copy (WC) path or Url:
- `WC` to `WC`: move and schedule for addition (with history), as a local change to
  be committed later (with or without further changes).
- `Url` to `Url`: complete server-side move, immediately creating a new revision in the repository.

## EXAMPLES

### Example 1
```powershell
{{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -AllowMixedRevisions
{{ Fill AllowMixedRevisions Description }}

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

### -Destination
{{ Fill Destination Description }}

```yaml
Type: SvnTarget
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
{{ Fill Force Description }}

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

### -Message
{{ Fill Message Description }}

```yaml
Type: String
Parameter Sets: Remote
Aliases: m

Required: True
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

### -Source
{{ Fill Source Description }}

```yaml
Type: SvnTarget[]
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### PoshSvn.SvnNotifyOutput

### PoshSvn.CmdLets.SvnCommitOutput

## NOTES

## RELATED LINKS
