---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnAdminCreate/
schema: 2.0.0
---

# Invoke-SvnCat

## SYNOPSIS

Gets the content of the item at the specified path, Url, and revision.

## SYNTAX

```
Invoke-SvnCat [[-Target] <SvnTarget>] [-AsByteStream] [-Raw] [<CommonParameters>]
```

## DESCRIPTION

The `svn-cat` cmdlet gets the content of the item at
the specific path, Url, and revision. For files, 
the content is read one line at a time and returns
a collection of objects, each representing a line of content.

## EXAMPLES

### Example 1

If you want to view README.md in your repository without checking it out:

```powershell
svn-cat https://svn.rinrab.com/rinrab/poshsvn/trunk/README.md

# Apache Subversion client for PowerShell

## Features

- Tab tab-completion (tab-expansion) for commands and parameters.
- Typed output of cmdlets.
- Formatted output to repeat the Subversion command line interface user experience.

## Installation

Install PoshSvn module from the PowerShell Gallery:

``powershell
Install-Module -Name PoshSvn
``

```

### Example 2

You can view specific versions of files, too.

````powershell
svn-cat https://svn.rinrab.com/rinrab/poshsvn/trunk/README.md -Revision 489

# Apache Subversion client for PowerShell

## Feuaptursubes

- Tab tab-completion (tab-expansion) for commands and parameters.
- Typed output of cmdlets.
...

````

## PARAMETERS

### -AsByteStream

Specifies that the content should be read as a stream of bytes.

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

### -Raw
Ignores newline characters and returns the entire contents of a file in one string with the newlines preserved. By default, newline characters in a file are used as delimiters to separate the input into an array of strings.

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

### System.String

### System.Byte

## NOTES

## RELATED LINKS

[Invoke-SvnList](https://www.poshsvn.com/docs/Invoke-SvnList/)

[Get-Content](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.management/get-content)
