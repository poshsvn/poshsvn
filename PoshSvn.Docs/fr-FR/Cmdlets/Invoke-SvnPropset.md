---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnPropset
schema: 2.0.0
---

# Invoke-SvnPropset

## SYNOPSIS
Set the value of a property on files, dirs, or revisions.

## SYNTAX

### Node (Default)
```
Invoke-SvnPropset [-PropertyName] <String> [-PropertyValue] <String> [[-Target] <SvnTarget[]>]
 [-ChangeList <String[]>] [-Depth <SvnDepth>] [<CommonParameters>]
```

### Revision
```
Invoke-SvnPropset [-PropertyName] <String> [-PropertyValue] <String> [[-Target] <SvnTarget[]>]
 [-RevisionProperty] -Revision <SvnRevision> [-Depth <SvnDepth>] [<CommonParameters>]
```

## DESCRIPTION
Set -PropertyName to -PropertyValue on files, directories, or revisions.
The first example creates a versioned, local property change in the working copy, and the second creates an unversioned, remote property change on a repository revision (TARGET determines only which repository to access).

## EXAMPLES

### Example 1
```
svn-propset svn:mime-type image/jpeg foo.jpg


    Properties on 'foo.jpg':

Name          Value
----          -----
svn:mime-type image/jpeg
```

### Example 2
```
svn-propset owner sally foo.c


    Properties on '.editorconfig':

Name  Value
----  -----
owner sally
```

### Example 2
```
svn-propset -RevisionProperty -Revision 26 svn:log "Document nap." http://svn.example.com/repos/


    Properties on 'http://svn.example.com/repos/':
    
Name    Value
----    -----
svn:log Document nap.
```

## PARAMETERS

### -ChangeList
{{ Fill ChangeList Description }}

```yaml
Type: String[]
Parameter Sets: Node
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

### -PropertyName
{{ Fill PropertyName Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases: propname

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PropertyValue
{{ Fill PropertyValue Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases: propval

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Revision
{{ Fill Revision Description }}

```yaml
Type: SvnRevision
Parameter Sets: Revision
Aliases: r, rev

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RevisionProperty
{{ Fill RevisionProperty Description }}

```yaml
Type: SwitchParameter
Parameter Sets: Revision
Aliases: revprop

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Target
{{ Fill Target Description }}

```yaml
Type: SvnTarget[]
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### PoshSvn.SvnTarget[]
## OUTPUTS

### PoshSvn.SvnProperty
## NOTES

## RELATED LINKS
