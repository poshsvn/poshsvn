---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
schema: 2.0.0
---

# Invoke-SvnLog

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### Target (Default)
```
Invoke-SvnLog [[-Target] <String[]>] [-Start <SvnRevision>] [-End <SvnRevision>] [-ChangedPaths]
 [-Limit <Int32>] [-Depth <SvnDepth>] [-IncludeExternals] [-WithAllRevisionProperties]
 [-WithNoRevisionProperties] [-WithRevisionProperties <String[]>] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### Path
```
Invoke-SvnLog [-Path <String[]>] [-Start <SvnRevision>] [-End <SvnRevision>] [-ChangedPaths] [-Limit <Int32>]
 [-Depth <SvnDepth>] [-IncludeExternals] [-WithAllRevisionProperties] [-WithNoRevisionProperties]
 [-WithRevisionProperties <String[]>] [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### Url
```
Invoke-SvnLog [-Url <Uri[]>] [-Start <SvnRevision>] [-End <SvnRevision>] [-ChangedPaths] [-Limit <Int32>]
 [-Depth <SvnDepth>] [-IncludeExternals] [-WithAllRevisionProperties] [-WithNoRevisionProperties]
 [-WithRevisionProperties <String[]>] [-ProgressAction <ActionPreference>] [<CommonParameters>]
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

### -ChangedPaths
{{ Fill ChangedPaths Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: v

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -End
{{ Fill End Description }}

```yaml
Type: SvnRevision
Parameter Sets: (All)
Aliases:

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

### -Limit
{{ Fill Limit Description }}

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: l

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

### -Start
{{ Fill Start Description }}

```yaml
Type: SvnRevision
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

### -WithAllRevisionProperties
{{ Fill WithAllRevisionProperties Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: with-all-revprops, WithAllRevprops

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WithNoRevisionProperties
{{ Fill WithNoRevisionProperties Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: with-no-revprops, WithNoRevprops

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WithRevisionProperties
{{ Fill WithRevisionProperties Description }}

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: with-revprop

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

### PoshSvn.CmdLets.SvnLogOutput

## NOTES

## RELATED LINKS
