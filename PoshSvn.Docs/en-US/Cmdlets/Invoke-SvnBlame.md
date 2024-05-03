---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnAdminCreate/
schema: 2.0.0
---

# Invoke-SvnBlame

## SYNOPSIS

Show author and revision information inline for the specified files or URLs.

## SYNTAX

```
Invoke-SvnBlame [[-Target] <SvnTarget>] [-Revision <SvnRevisionRange>] [-RetrieveMergedRevisions]
 [-IgnoreMimeType] [-IgnoreLineEndings] [-IgnoreSpacing <SvnIgnoreSpacing>] [<CommonParameters>]
```

## DESCRIPTION

Annotate each line of a file with the revision number and author
of the last change (or optionally the next change) to that line.

## EXAMPLES

### Example 1

If you want to see blame-annotated source for README file in your repository:

```powershell
svn-blame https://svn.example.com/repos/README

       r3 sally            This is a README file.
       r1 harry            Don't bother reading it.  The boss is a knucklehead.
       r3 sally
```

### Example 2

If you format output as list, you can get more detalized blame annotations:

```powershell
svn-blame https://svn.example.com/repos/README | Format-List

Revision                 : 3
Author                   : sally
Line                     : This is a README file.
MergedRevisionProperties :
RevisionProperties       :
LocalChange              : False
MergedRevision           :
MergedTime               :
MergedPath               :
MergedAuthor             :
Time                     : 3/25/2024 9:34:15 PM
LineNumber               : 0
EndRevision              : 7
StartRevision            : 0

...
```

## PARAMETERS

### -IgnoreLineEndings

Ignore line endings.

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

### -IgnoreMimeType
Ignore mime type.

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

### -IgnoreSpacing
Ignore spacing.

```yaml
Type: SvnIgnoreSpacing
Parameter Sets: (All)
Aliases:
Accepted values: None, IgnoreSpace, IgnoreAll

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RetrieveMergedRevisions
Retrives merged revisions and revision properties.

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

### -Revision
Specifies revision to operate.

```yaml
Type: SvnRevisionRange
Parameter Sets: (All)
Aliases: rev, r

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Target
Specifies target to operate.

```yaml
Type: SvnTarget
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

### None

## OUTPUTS

### PoshSvn.SvnBlameLine

## NOTES

## RELATED LINKS
