---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnAdminCreate/
schema: 2.0.0
---

# Invoke-SvnCat

## SYNOPSIS

Obtient le contenu de l'élément au chemin, à l'URL et à la révision spécifiés.

## SYNTAX

```
Invoke-SvnCat [[-Target] <SvnTarget>] [-AsByteStream] [-Raw] [<CommonParameters>]
```

## DESCRIPTION

Le cmdlet `svn-cat` récupère le contenu de l'élément au chemin, à l'URL et à la révision spécifiés. Pour les fichiers, le contenu est lu ligne par ligne et renvoie une collection d'objets, chacun représentant une ligne de contenu.

## EXAMPLES

### Exemple 1

Si vous souhaitez visualiser le fichier README.md dans votre dépôt sans le vérifier :

```powershell
svn-cat https://svn.rinrab.com/rinrab/poshsvn/trunk/README.md

# Client Apache Subversion pour PowerShell

## Fonctionnalités

- Complétion automatique (expansion de tabulation) des commandes et des paramètres.
- Sortie typée des cmdlets.
- Sortie formatée pour reproduire l'expérience utilisateur de l'interface de ligne de commande Subversion.

## Installation

Installez le module PoshSvn depuis la galerie PowerShell :

``powershell
Install-Module -Name PoshSvn
``
```

### Exemple 2

Vous pouvez également visualiser des versions spécifiques de fichiers.

````powershell
svn-cat https://svn.rinrab.com/rinrab/poshsvn/trunk/README.md -Revision 489

# Client Apache Subversion pour PowerShell

## Fonctionnalités

- Complétion automatique (expansion de tabulation) des commandes et des paramètres.
- Sortie typée des cmdlets.
...

````

## PARAMETERS

### -AsByteStream
Spécifie que le contenu doit être lu comme un flux d'octets.

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
Ignore les caractères de saut de ligne et renvoie l'intégralité du contenu d'un fichier dans une seule chaîne avec les sauts de ligne préservés. Par défaut, les caractères de saut de ligne dans un fichier sont utilisés comme délimiteurs pour séparer l'entrée en un tableau de chaînes.

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
Spécifie la cible sur laquelle opérer.

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
