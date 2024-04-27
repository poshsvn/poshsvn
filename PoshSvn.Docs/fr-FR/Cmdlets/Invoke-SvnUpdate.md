---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnUpdate/
schema: 2.0.0
---

# Invoke-SvnUpdate

## SYNOPSIS
Actualiser votre copie de travail avec les dernières modifications du référentiel.

## SYNTAX

```
Invoke-SvnUpdate [[-Path] <String[]>] [-Revision <SvnRevision>] [<CommonParameters>]
```

## DESCRIPTION
Récupérer les modifications du référentiel dans la copie de travail. Si aucune révision n'est donnée, mettre à jour la copie de travail avec la révision HEAD. Sinon, synchroniser la copie de travail avec la révision spécifiée par -Revision.

## EXAMPLES

### Example 1
```powershell
{{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Path
Spécifie le chemin de l'élément à mettre à jour. Par défaut, c'est `.` (le répertoire actuel).

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Revision
Spécifie la révision à laquelle effectuer la mise à jour.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### PoshSvn.CmdLets.SvnUpdateOutput

## NOTES

## RELATED LINKS
