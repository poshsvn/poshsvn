---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnCleanup/
schema: 2.0.0
---

# Invoke-SvnCleanup

## SYNOPSIS
Clean up a working copy.

## SYNTAX

```
Invoke-SvnCleanup [[-Path] <String[]>] [-RemoveUnversioned] [-RemoveIgnored] [-VacuumPristines]
 [-IncludeExternals] [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION

1. When none of the options `-RemoveUnversioned`, `-RemoveIgnored`, and
`-VacuumPristines` is specified, Remove locks all write lock
from the working copy.

2. If the `-RemoveUnversioned` option or the `-RemoveIgnored` option
is given, remove any unversioned or ignored items.

3. If the -VacuumPristines option is given, remove pristine copies of
files which are stored inside the `.svn` directory and which are no longer
referenced by any file in the working copy.

## EXAMPLES

### Example 1: Clean up a working copy

Clean up the working copy located at `C:\path\to\wc`. Usually, this is only
necessary if a Subversion client has crashed while using the working copy,
leaving it in an unusable state:

```powershell
svn-cleanup C:\path\to\wc
```

## PARAMETERS

### -IncludeExternals
Also operate on externals defined by `svn:externals` properties.

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
Path to a working copy to clean up.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveIgnored
Remove ignored items.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: remove-ignored

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveUnversioned
Remove unversioned items.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: remove-unversioned

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -VacuumPristines
Remove unreferenced pristines from .svn directory.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: vacuum-pristines

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

### PoshSvn.CmdLets.SvnCommitOutput

## NOTES

## RELATED LINKS
