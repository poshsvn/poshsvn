---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnDelete/
schema: 2.0.0
---

# Invoke-SvnDiff

## SYNOPSIS
This displays the differences between two revisions or paths.

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
Display the differences between two Targets. You can use svn-diff in the following ways:

- Use just svn diff to display local modifications in a working copy.
- Display the differences between two Targets,
  specified by `-Old` and `-New` parameters.
  The targets may be differ by Path, Url, and Revision.
  You may find more information about PoshSvn Targets system here: [about_PoshSvnTarget](https://www.poshsvn.com/docs/about_PoshSvnTarget/).

## EXAMPLES

### Example 1

Compare `BASE` and your working copy (one of the most popular uses of `svn-diff`):

```powershell
svn-diff COMMITTERS.md
Index: COMMITTERS.md
===================================================================
--- COMMITTERS.md	(revision 4404)
+++ COMMITTERS.md	(working copy)
...
```

### Example 2

Compare `BASE` and your working copy (one of the most popular uses of `svn-diff`):

```powershell
svn-diff -Old https://svn.example.com/repos/trunk/COMMITTERS@3000 `
         -New https://svn.example.com/repos/trunk/COMMITTERS@3000

Index: COMMITTERS
===================================================================
--- COMMITTERS	(revision 3000)
+++ COMMITTERS	(revision 3500)
```

### Example 3

Or you may use a dictionary with parameters.

```powershell
$parameters = @{
    Old = "https://svn.example.com/repos/trunk/COMMITTERS@3000"
    New = "https://svn.example.com/repos/trunk/COMMITTERS@3500"
}
svn-diff $parameters

Index: COMMITTERS
===================================================================
--- COMMITTERS	(revision 3000)
+++ COMMITTERS	(revision 3500)
```

## PARAMETERS

### -Changelist
Specifies a changlist to operate.

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

### -Git
Use git's extended diff format.

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
Ignore properties during the operation

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
Specifies the newer target.

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
Do not print differences for added files.

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
Do not print differences for deleted files.

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
Diff unrelated nodes as delete and add.

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
Specifies the newer target.

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
Generate diff suitable for generic third-party patch tools.
Currently the same as `-ShowCopiesAsAdds` `-IgnoreProperties`.

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
Show only properties during the operation.

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
Don't diff copied or moved files with their source.

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
Specifies target to operate.

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
