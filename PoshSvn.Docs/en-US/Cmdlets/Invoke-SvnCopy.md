---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnCopy
schema: 2.0.0
---

# Invoke-SvnCopy

## SYNOPSIS
Copy files and directories in a working copy or repository.

## SYNTAX

### Local (Default)
```
Invoke-SvnCopy [-Source] <SvnTarget[]> [-Destination] <SvnTarget> [-Revision <SvnRevision>] [-IgnoreExternals]
 [-Parents] [<CommonParameters>]
```

### Remote
```
Invoke-SvnCopy [-Source] <SvnTarget[]> [-Destination] <SvnTarget> -Message <String> [-Revision <SvnRevision>]
 [-IgnoreExternals] [-Parents] [<CommonParameters>]
```

## DESCRIPTION
Source and Destination can each be either a working copy (WC) path or Url:
- `WC` to `WC`: copy and schedule for addition (with history)
- `WC` to `Url`: immediately commit a copy of WC to Url
- `Url` to `WC`: check out Url into WC, schedule for addition
- `Url` to `Url`: complete server-side copy;  used to branch and tag

All the Sources must be of the same type. If Destination is an existing directory,
the sources will be added as children of Destination. When copying multiple
sources, Destination must be an existing directory.

## EXAMPLES

### Example 1
```powershell
svn-copy foo.txt bar.txt

A       bar.txt

svn-status

Status  Path
------  ----
A  +    b.txt
```

This command copies an item within your working copy (this schedules the copy-nothing goes into the repository until you commit).

### Example 2
```powershell
svn-copy -Source bat.c, baz.c, qux.c -Destination src

A        src/bat.c
A        src/baz.c
A        src/qux.c
```

This command copies several files in a working copy into a directory.

### Example 3
```powershell
svn-copy -Revision 8 bat.c ya-old-bat.c

A        ya-old-bat.c
```

This command copies revision 8 of `bat.c` into your working copy under a different name.

### Example 4
```powershell
svn-copy -Source near.txt -Destination https://svn.example.com/repos/far-away.txt -Message "Remote copy."

Committed revision 8.
```

This command copies an item in your working copy to a URL in the repository (this is an immediate commit, so you must supply a commit message).

### Example 5
```powershell
svn-copy https://svn.example.com/repos/far-away .\near-here -Revision 6

A         near-here
```

This command copies item from the repository to your working copy (this just schedules the copy-nothing goes into the repository until you commit).

Tip: This is the recommended way to resurrect a dead file in your repository!

### Example 6

```powershell
$src = https://svn.example.com/repos/far-away
$dst = https://svn.example.com/repos/over-there

svn-copy -Source $src -Destination $dst -Message "remote copy."

Committed revision 9.
```

This command does copy between two remote Urls. You may use variables to simplify your command.

### Example 7

```powershell
$root = "https://svn.example.com/repos/test"
svn-copy -Source "$root/trunk" -DestinationUrl "$root/tags/0.6.32-prerelease" -Message "tag tree"

Committed revision 12.
```

This command creates a tag of a repository. You may use `-DestinationUrl` parameter, to specify that this is remote operation.

## PARAMETERS

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

### -IgnoreExternals
{{ Fill IgnoreExternals Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: ignore-externals

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
