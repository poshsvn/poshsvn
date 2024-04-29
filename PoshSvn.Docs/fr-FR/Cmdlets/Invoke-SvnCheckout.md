---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnCheckout/
schema: 2.0.0
---

# Invoke-SvnCheckout

## SYNOPSIS
Extraire une copie de travail à partir d'un dépôt.

## SYNTAX

```
Invoke-SvnCheckout [-Url] <Uri> [[-Path] <String>] [-Revision <SvnRevision>] [-IgnoreExternals] [-Force]
 [<CommonParameters>]
```

## DESCRIPTION
Extraire une copie de travail à partir d'un dépôt.

## EXAMPLES

### Exemple 1 : Extraire une copie de travail

Extraire le dépôt serf dans le répertoire 'serf-trunk' :

```powershell
svn-checkout https://svn.apache.org/repos/asf/serf/trunk serf-trunk
```

## PARAMETERS

### -Force
Forcer l'exécution de l'opération.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: f

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IgnoreExternals
Indique à Subversion d'ignorer les définitions externes et les copies de travail externes gérées par elles.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: ignore-externals

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Spécifie un chemin d'une copie de travail. Si le CHEMIN est omis, le nom de base de l'URL sera utilisé comme destination.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Revision
Spécifie une révision avec laquelle opérer. Vous pouvez fournir des numéros de révision, des mots-clés ou des dates (entre accolades) comme arguments à l'option de révision. Si vous souhaitez offrir une plage de révisions, vous pouvez fournir deux révisions séparées par un deux-points.

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

### -Url
Spécifie une URL d'un dépôt. Si plusieurs URL sont fournies, chacune sera extraite dans un sous-répertoire de CHEMIN, le nom du sous-répertoire étant le nom de base de l'URL.

```yaml
Type: Uri
Parameter Sets: (All)
Aliases:

Required: True
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

### PoshSvn.CmdLets.SvnCheckoutOutput

## NOTES

## RELATED LINKS

[svnadmin-create](https://www.poshsvn.com/docs/Invoke-SvnAdminCreate/)
