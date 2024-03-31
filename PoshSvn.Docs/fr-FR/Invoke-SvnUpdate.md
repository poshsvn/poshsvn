---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnUpdate/
schema: 2.0.0
---

# Invoke-SvnUpdate

## SYNOPSIS
Actualiser votre copie de travail avec les derni�res modifications du r�f�rentiel.

## SYNTAX

```
Invoke-SvnUpdate [[-Path] <String[]>] [-Revision <SvnRevision>] [<CommonParameters>]
```

## DESCRIPTION
R�cup�rer les modifications du r�f�rentiel dans la copie de travail. Si aucune r�vision n'est donn�e, mettre � jour la copie de travail avec la r�vision HEAD. Sinon, synchroniser la copie de travail avec la r�vision sp�cifi�e par -Revision.

## EXAMPLES

### Example 1
```powershell
{{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Path
Sp�cifie le chemin de l'�l�ment � mettre � jour. Par d�faut, c'est `.` (le r�pertoire actuel).

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
Sp�cifie la r�vision � laquelle effectuer la mise � jour.

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
