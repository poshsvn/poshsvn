---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnDelete/
schema: 2.0.0
---

# Invoke-SvnDiff

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### Target (Default)
```
Invoke-SvnDiff [[-Target] <SvnTarget[]>] [-Depth <SvnDepth>] [-NoDiffAdded] [-NoDiffDeleted]
 [-IgnoreProperties] [-PropertiesOnly] [-ShowCopiesAsAdds] [-NoticeAncestry] [-Changelist <String>] [-Git]
 [-PatchCompatible] [<CommonParameters>]
```

### TwoFiles
```
Invoke-SvnDiff [-Old <SvnTarget>] [-New <SvnTarget>] [-Depth <SvnDepth>] [-NoDiffAdded] [-NoDiffDeleted]
 [-IgnoreProperties] [-PropertiesOnly] [-ShowCopiesAsAdds] [-NoticeAncestry] [-Changelist <String>] [-Git]
 [-PatchCompatible] [<CommonParameters>]
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

### -Changelist
{{ Fill Changelist Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases: cl

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

### -Git
{{ Fill Git Description }}

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

### -IgnoreProperties
{{ Fill IgnoreProperties Description }}

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

### -New
{{ Fill New Description }}

```yaml
Type: SvnTarget
Parameter Sets: TwoFiles
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoDiffAdded
{{ Fill NoDiffAdded Description }}

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

### -NoDiffDeleted
{{ Fill NoDiffDeleted Description }}

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

### -NoticeAncestry
{{ Fill NoticeAncestry Description }}

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

### -Old
{{ Fill Old Description }}

```yaml
Type: SvnTarget
Parameter Sets: TwoFiles
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PatchCompatible
{{ Fill PatchCompatible Description }}

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

### -PropertiesOnly
{{ Fill PropertiesOnly Description }}

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

### -ShowCopiesAsAdds
{{ Fill ShowCopiesAsAdds Description }}

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
Parameter Sets: Target
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

### PoshSvn.SvnTarget[]

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS
