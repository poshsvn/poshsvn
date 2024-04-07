---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnExport/
schema: 2.0.0
---

# Invoke-SvnExport

## SYNOPSIS

Create an unversioned copy of a tree.

## SYNTAX

```
Invoke-SvnExport [-Source] <SvnTarget> [[-Destination] <String>] [-Revision <PoshSvnRevision>]
 [-Depth <SvnDepth>] [-Force] [-IgnoreExternals] [-IgnoreKeywords] [<CommonParameters>]
```

## DESCRIPTION

Exports file or directory from repository or working copy as unversioned.

## EXAMPLES

### Example 1


### Example 1

Download serf release of version 1.3.10 into the `serf-1.3.10` directory:

```powershell
svn-export https://svn.apache.org/repos/asf/serf/tags/1.3.10/ serf-1.3.10

A       serf-1.3.10
A       serf-1.3.10\buckets
A       serf-1.3.10\test
A       serf-1.3.10\test\server
A       serf-1.3.10\test\testcases
A       serf-1.3.10\CHANGES
...
```

Additionaly, you may export, create ZIP, and compute the hash value for of the downloaded sources:

```powershell
svn-export https://svn.apache.org/repos/asf/serf/tags/1.3.10/ serf-1.3.10
Compress-Archive -Path .\serf-1.3.10\ -DestinationPath .\serf-1.3.10.zip
Get-FileHash .\serf-1.3.10.zip -Algorithm SHA1 | Select-Object Hash, Algorithm # We need only Hash and Algoritm
# Hash                                     Algorithm
# ----                                     ---------
# 87B1A5983AEFB11E382335F08AE6F408159D94B3 SHA1
```

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

### -Destination
Specifies the destination path of an item.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: .
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Force operation to run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: f

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IgnoreExternals
Tells Subversion to ignore externals definitions and the external working copies managed by them.

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

### -IgnoreKeywords
Tells Subversion to ignore keywords.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: ignore-keywords

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Revision
Specifies revision to operate.

```yaml
Type: PoshSvnRevision
Parameter Sets: (All)
Aliases: rev, r

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Source
Specifies the source path or url of an item.

```yaml
Type: SvnTarget
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

## NOTES

## RELATED LINKS
