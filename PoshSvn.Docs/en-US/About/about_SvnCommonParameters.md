---
description: Describes the parameters that can be used with any PoshSvn client cmdlet.
Locale: en-US
online version: https://www.poshsvn.com/docs/about_SvnCommonParameters/
schema: 2.0.0
title: about CommonParameters
---

# PoshSvn
## about_SvnCommonParameters

## Short description

Describes the parameters that can be used with any PoshSvn client cmdlet.

## Long description

The common PoshSvn client parameters are parameters that may be used with all
subversion client cmdlets.

## Common parameter descriptions

### -NoAuthCache
Do not cache authentication tokens.

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
Specifies a password.

Note: to convert a password from a string to a SecureString, you may use the
[Read-Host](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/read-host?view=powershell-7.4)
cmdlet with `-AsSecureString` option enabled.

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

### -Username
Specifies a username.

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
