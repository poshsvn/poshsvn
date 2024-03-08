---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version:
schema: 2.0.0
---

# Invoke-SvnCheckout

## SYNOPSIS
Check out a working copy from a repository.

## SYNTAX

```
Invoke-SvnCheckout [-Url] <Uri> [[-Path] <String>] [-Revision <SvnRevision>] [-IgnoreExternals] [-Force]
 [-NoAuthCache] [-Username <String>] [-Password <SecureString>] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

## DESCRIPTION
Check out a working copy from a repository.

## EXAMPLES

### Example 1
```powershell
PS C:\> svn-checkout https://svn.apache.org/repos/asf/serf/trunk serf-trunk
```

Checks out the serf repository into 'serf-trunk' directory.

## PARAMETERS

### -Force
Forces a particular command or operation to run. Subversion will prevent you from performing some operations in normal usage, but you can pass this option to tell Subversion "I know what I'm doing as well as the possible repercussions of doing it, so let me at 'em." This option is the programmatic equivalent of doing your own electrical work with the power on-if you don't know what you're doing, you're likely to get a nasty shock.

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

### -NoAuthCache
Prevents caching of authentication information (e.g., username and password) in the Subversion runtime configuration directories.

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

### -Password
Specifies the password to use when authenticating against a Subversion server. If not provided, or if incorrect, Subversion will prompt you for this information as needed.

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Specifies a Path of a working copy. If PATH is omitted, the basename of the URL will be used as the destination.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Revision
Specifies a revision on with which to operate. You can provide revision numbers, keywords, or dates (in curly braces) as arguments to the revision option. If you wish to offer a range of revisions, you can provide two revisions separated by a colon.

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

### -Url
Specifies an URL of a repository. If multiple URLs are given, each will be checked out into a subdirectory of PATH, with the name of the subdirectory being the basename of the URL.

```yaml
Type: Uri
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Username
Specifies the username to use when authenticating against a Subversion server. If not provided, or if incorrect, Subversion will prompt you for this information as needed.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProgressAction
{{ Fill ProgressAction Description }}

```yaml
Type: ActionPreference
Parameter Sets: (All)
Aliases: proga

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### PoshSvn.CmdLets.SvnCheckoutOutput

## NOTES

## RELATED LINKS
