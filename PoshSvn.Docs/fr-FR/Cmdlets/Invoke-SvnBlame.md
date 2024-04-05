---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnAdminCreate/
schema: 2.0.0
---

# Invoke-SvnBlame

## SYNOPSIS

Affiche les informations sur l'auteur et la révision en ligne pour les fichiers ou les URL spécifiés.

## SYNTAX

```
Invoke-SvnBlame [[-Target] <SvnTarget>] [-Revision <PoshSvnRevisionRange>] [-RetrieveMergedRevisions]
 [-IgnoreMimeType] [-IgnoreLineEndings] [-IgnoreSpacing <SvnIgnoreSpacing>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### Exemple 1

Si vous voulez voir le code source annoté pour le fichier README dans votre dépôt :

```powershell
svn-blame https://svn.example.com/repos/README

       r3 sally            Ceci est un fichier README.
       r1 harry            Ne vous embêtez pas à le lire. Le patron est un crétin.
       r3 sally
```

### Exemple 2

Si vous formatez la sortie sous forme de liste, vous pouvez obtenir des annotations d'attribution plus détaillées :

```powershell
svn-blame https://svn.example.com/repos/README | Format-List

Révision                 : 3
Auteur                   : sally
Ligne                    : Ceci est un fichier README.
PropriétésRévisionMerge : 
PropriétésRévision       : 
ChangementLocal          : False
RévisionFusionnée        : 
HeureFusion              : 
CheminFusion             : 
AuteurFusion             : 
Heure                    : 25/03/2024 21:34:15
NuméroLigne              : 0
RévisionFin              : 7
RévisionDébut            : 0

...
```

## PARAMETERS

### -IgnoreLineEndings
Ignore les fins de ligne.

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

### -IgnoreMimeType
Ignore le type MIME.

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

### -IgnoreSpacing
Ignore les espaces.

```yaml
Type: SvnIgnoreSpacing
Parameter Sets: (All)
Aliases:
Accepted values: None, IgnoreSpace, IgnoreAll

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RetrieveMergedRevisions
Récupère les révisions fusionnées et les propriétés de révision.

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
Spécifie la révision sur laquelle opérer.

```yaml
Type: PoshSvnRevisionRange
Parameter Sets: (All)
Aliases: rev, r

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

### PoshSvn.SvnBlameLine

## NOTES

## RELATED LINKS
