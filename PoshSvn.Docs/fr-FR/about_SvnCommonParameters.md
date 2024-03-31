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

Description des paramètres pouvant être utilisés avec n'importe quelle cmdlet client PoshSvn.

## Long description

Les paramètres communs du client PoshSvn sont des paramètres qui peuvent être utilisés avec toutes les cmdlets client Subversion.

## Descriptions des paramètres communs

### -NoAuthCache
Ne pas mettre en cache les jetons d'authentification.

```yaml
Type: SwitchParameter
Ensembles de paramètres: (Tous)
Alias:

Requis: Faux
Position: Nommé
Valeur par défaut: Aucune
Accepter l'entrée du pipeline: Faux
Accepter les caractères génériques: Faux
```

### -Password
Spécifie un mot de passe.

Remarque : pour convertir un mot de passe d'une chaîne en SecureString, vous pouvez utiliser la cmdlet [Read-Host](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/read-host?view=powershell-7.4) avec l'option `-AsSecureString` activée.

```yaml
Type: SecureString
Ensembles de paramètres: (Tous)
Alias:

Requis: Faux
Position: Nommé
Valeur par défaut: Aucune
Accepter l'entrée du pipeline: Faux
Accepter les caractères génériques: Faux
```

### -Username
Spécifie un nom d'utilisateur.

```yaml
Type: String
Ensembles de paramètres: (Tous)
Alias:

Requis: Faux
Position: Nommé
Valeur par défaut: Aucune
Accepter l'entrée du pipeline: Faux
Accepter les caractères génériques: Faux
```