---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
schema: 2.0.0
---

# Invoke-SvnInfo

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### Target (Default)
```
Invoke-SvnInfo [[-Target] <String[]>] [-Revision <SvnRevision>] [-Depth <SvnDepth>] [-IncludeExternals]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### Path
```
Invoke-SvnInfo [-Path <String[]>] [-Revision <SvnRevision>] [-Depth <SvnDepth>] [-IncludeExternals]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### Url
```
Invoke-SvnInfo [-Url <Uri[]>] [-Revision <SvnRevision>] [-Depth <SvnDepth>] [-IncludeExternals]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Depth
{{ Fill Depth Description }}

```yaml
Type: SvnDepth
Parameter Sets: (All)
Aliases:
Accepted values: Empty, Files, Immediates, Infinity

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeExternals
{{ Fill IncludeExternals Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: include-externals

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
{{ Fill Path Description }}

```yaml
Type: String[]
Parameter Sets: Path
Aliases:

Required: False
Position: Named
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

### -Revision
{{ Fill Revision Description }}

```yaml
Type: SvnRevision
Parameter Sets: (All)
Aliases: rev

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Target
{{ Fill Target Description }}

```yaml
Type: String[]
Parameter Sets: Target
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
{{ Fill Url Description }}

```yaml
Type: Uri[]
Parameter Sets: Url
Aliases:

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

### PoshSvn.CmdLets.SvnInfoOutput

## NOTES

## RELATED LINKS
