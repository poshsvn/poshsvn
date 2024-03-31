---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnList/
schema: 2.0.0
---

# Invoke-SvnList

## SYNOPSIS
Liste les entr�es de r�pertoire dans le r�pertoire sp�cifi�.

## SYNTAX

```
Invoke-SvnList [[-Target] <SvnTarget[]>] [-Detailed] [-Revision <SvnRevision>] [-Depth <SvnDepth>]
 [-IncludeExternals] [<CommonParameters>]
```

## DESCRIPTION
Liste chaque fichier de la cible et le contenu de chaque r�pertoire de la cible tels qu'ils existent dans le r�f�rentiel. La cible peut �tre une URL ou un chemin.

## EXAMPLES

### Example 1
```powershell
{{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Depth
Limitez la port�e de l'op�ration en sp�cifiant la profondeur (Vide, Fichiers, Imm�diats ou Infini).

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
Affiche des informations suppl�mentaires.

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
Indique � Subversion d'inclure les d�finitions externes et les copies de travail externes g�r�es par elles.

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
Sp�cifie une r�vision avec laquelle op�rer.

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
Sp�cifie le chemin ou l'URL du r�pertoire � r�pertorier.

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
