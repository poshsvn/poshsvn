---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnList/
schema: 2.0.0
---

# Invoke-SvnList

## SYNOPSIS
Liste les entrées de répertoire dans le répertoire spécifié.

## SYNTAX

```
Invoke-SvnList [[-Target] <SvnTarget[]>] [-Detailed] [-Revision <SvnRevision>] [-Depth <SvnDepth>]
 [-IncludeExternals] [<CommonParameters>]
```

## DESCRIPTION
Liste chaque fichier de la cible et le contenu de chaque répertoire de la cible tels qu'ils existent dans le référentiel. La cible peut être une URL ou un chemin.

## EXAMPLES

### Example 1
```powershell
{{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Depth
Limitez la portée de l'opération en spécifiant la profondeur (Vide, Fichiers, Immédiats ou Infini).

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

### -Detailed
Affiche des informations supplémentaires.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: v

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IncludeExternals
Indique à Subversion d'inclure les définitions externes et les copies de travail externes gérées par elles.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: include-externals

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Revision
Spécifie une révision avec laquelle opérer.

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

### -Target
Spécifie le chemin ou l'URL du répertoire à répertorier.

```yaml
Type: SvnTarget[]
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

### System.String[]

## OUTPUTS

### PoshSvn.SvnItem

### PoshSvn.SvnItemDetailed

## NOTES

## RELATED LINKS
