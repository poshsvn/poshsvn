---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnAdminCreate/
schema: 2.0.0
---

# Invoke-SvnBlame

## SYNOPSIS

Affiche les informations sur l'auteur et la r�vision en ligne pour les fichiers ou les URL sp�cifi�s.

## SYNTAX

```
Invoke-SvnBlame [[-Target] <SvnTarget>] [-Revision <PoshSvnRevisionRange>] [-RetrieveMergedRevisions]
 [-IgnoreMimeType] [-IgnoreLineEndings] [-IgnoreSpacing <SvnIgnoreSpacing>] [<CommonParameters>]
```

## DESCRIPTION

## EXAMPLES

### Exemple 1

Si vous voulez voir le code source annot� pour le fichier README dans votre d�p�t :

```powershell
svn-blame https://svn.example.com/repos/README

       r3 sally            Ceci est un fichier README.
       r1 harry            Ne vous emb�tez pas � le lire. Le patron est un cr�tin.
       r3 sally
```

### Exemple 2

Si vous formatez la sortie sous forme de liste, vous pouvez obtenir des annotations d'attribution plus d�taill�es :

```powershell
svn-blame https://svn.example.com/repos/README | Format-List

R�vision                 : 3
Auteur                   : sally
Ligne                    : Ceci est un fichier README.
Propri�t�sR�visionMerge : 
Propri�t�sR�vision       : 
ChangementLocal          : False
R�visionFusionn�e        : 
HeureFusion              : 
CheminFusion             : 
AuteurFusion             : 
Heure                    : 25/03/2024 21:34:15
Num�roLigne              : 0
R�visionFin              : 7
R�visionD�but            : 0

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
R�cup�re les r�visions fusionn�es et les propri�t�s de r�vision.

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
Sp�cifie la r�vision sur laquelle op�rer.

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
Sp�cifie la cible sur laquelle op�rer.

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
