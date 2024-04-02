---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnLog/
schema: 2.0.0
---

# Invoke-SvnLog

## SYNOPSIS
Show the log messages for a set of revision(s) and/or path(s).

## SYNTAX

```
Invoke-SvnLog [[-Target] <SvnTarget[]>] [-Revision <PoshSvnRevisionRange[]>] [-ChangedPaths] [-Limit <Int32>]
 [-Depth <SvnDepth>] [-IncludeExternals] [-WithAllRevisionProperties] [-WithNoRevisionProperties]
 [-WithRevisionProperties <String[]>] [<CommonParameters>]
```

## DESCRIPTION
Show the log messages from the repository. If no target supplied, `svn-log` shows the log messages for all files and directories inside the current working directory of your working copy.

You may supply string with Url or Path to the target parameter or specify them manualy to the `-Url` or `-Path` parameters.

## EXAMPLES

### Example 1
```powershell
svn-log

------------------------------------------------------------------------
r20        harry               2003-01-17 22:56 -06:00

Tweak.
------------------------------------------------------------------------
r17        sally               2003-01-16 23:21 -06:00
...
```

This command show the log messages the current directory and all items inside.

### Example 2
```powershell
svn-log foo.c

------------------------------------------------------------------------
r32        sally               2003-01-13 00:43 -06:00

Added defines.
------------------------------------------------------------------------
r28        sally               2003-01-07 21:48 -06:00
...
```

This command show the log messages of the specific file, in this example `foo.c`.

### Example 3
```powershell
svn-log https://svn.example.com/repos/foo.c

------------------------------------------------------------------------
r32        sally               2003-01-13 00:43 -06:00

Added defines.
------------------------------------------------------------------------
r28        sally               2003-01-07 21:48 -06:00
...
```

This command show the log messages of the remote item by Url.

### Example 4
```powershell
svn-log https://svn.example.com/repos/foo.c https://svn.example.com/repos/bar.c

------------------------------------------------------------------------
r32        sally               2003-01-13 00:43 -06:00

Added defines.
------------------------------------------------------------------------
r31        harry               2003-01-10 12:25 -06:00

Added new file bar.c
------------------------------------------------------------------------
r28        sally               2003-01-07 21:48 -06:00
...
```

This command show the log messages of `foo.c` and `bar.c` inside the repository `https://svn.example.com/repos`.

Important: the items has to be in the same repository.

### Example 5
```powershell
svn-log | Format-Table

  Revision Author           Date                   Message
  -------- ------           ----                   -------
        32 sally            2003-01-13    00:43    Added defines.
        31 harry            2003-01-10    12:25    Added new file bar.c
        28 sally            2003-01-07    21:48    ...
```

This command show the log messages and formats them to table. You may use it to get more compact output.

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

### -Limit
Specifies the maximum number of log entries.

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

### -Revision
{{ Fill Revision Description }}

```yaml
Type: PoshSvnRevisionRange[]
Parameter Sets: (All)
Aliases: rev, r

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Target
Specifies the Path or the Url of an item to show log. Path and Url targets cannot be combined in one invokation.

```yaml
Type: SvnTarget[]
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: .
Accept pipeline input: False
Accept wildcard characters: False
```

### -WithAllRevisionProperties
Retrieve all revision properties.

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
Retrieve no revision properties.

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
Retrieve specified revision properties.

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
