---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnList/
schema: 2.0.0
---

# Invoke-SvnList

## SYNOPSIS
List directory entries in the directory.

## SYNTAX

### Target (Default)
```
Invoke-SvnList [[-Target] <String[]>] [-Detailed] [-Revision <SvnRevision>] [-Depth <SvnDepth>]
 [-IncludeExternals] [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### Path
```
Invoke-SvnList [-Path <String[]>] [-Detailed] [-Revision <SvnRevision>] [-Depth <SvnDepth>] [-IncludeExternals]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### Url
```
Invoke-SvnList [-Url <Uri[]>] [-Detailed] [-Revision <SvnRevision>] [-Depth <SvnDepth>] [-IncludeExternals]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
List each Target file and the contents of each TARGET directory as they exist in the repository. Target can be Url or Path.

## EXAMPLES

### Example 1
```powershell
{{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Depth
Limit the scope of the operation by specified depth (Empty, Files, Immediates, or Infinity).

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

### -Detailed
{{ Fill Detailed Description }}

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

### -IncludeExternals
Tells Subversion to include externals definitions and the external working copies managed by them.

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
Specifies a Path to the directory to list.

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

### -Revision
Specifies a revision on with which to operate.

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
Specifies the Path or the Url of the directory to list.

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
Specifies the Url of the directory to list.

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

### PoshSvn.SvnItem

### PoshSvn.SvnItemDetailed

## NOTES

## RELATED LINKS
