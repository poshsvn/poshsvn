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

Description des param�tres pouvant �tre utilis�s avec n'importe quelle cmdlet client PoshSvn.

## Long description

Les param�tres communs du client PoshSvn sont des param�tres qui peuvent �tre utilis�s avec toutes les cmdlets client Subversion.

## Descriptions des param�tres communs

### -NoAuthCache
Ne pas mettre en cache les jetons d'authentification.

```yaml
Type: SwitchParameter
Ensembles de param�tres: (Tous)
Alias:

Requis: Faux
Position: Nomm�
Valeur par d�faut: Aucune
Accepter l'entr�e du pipeline: Faux
Accepter les caract�res g�n�riques: Faux
```

### -Password
Sp�cifie un mot de passe.

Remarque : pour convertir un mot de passe d'une cha�ne en SecureString, vous pouvez utiliser la cmdlet [Read-Host](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/read-host?view=powershell-7.4) avec l'option `-AsSecureString` activ�e.

```yaml
Type: SecureString
Ensembles de param�tres: (Tous)
Alias:

Requis: Faux
Position: Nomm�
Valeur par d�faut: Aucune
Accepter l'entr�e du pipeline: Faux
Accepter les caract�res g�n�riques: Faux
```

### -Username
Sp�cifie un nom d'utilisateur.

```yaml
Type: String
Ensembles de param�tres: (Tous)
Alias:

Requis: Faux
Position: Nomm�
Valeur par d�faut: Aucune
Accepter l'entr�e du pipeline: Faux
Accepter les caract�res g�n�riques: Faux
```