---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnCheckout/
schema: 2.0.0
---

# Invoke-SvnCheckout

## SYNOPSIS
Extraire une copie de travail � partir d'un d�p�t.

## SYNTAX

```
Invoke-SvnCheckout [-Url] <Uri> [[-Path] <String>] [-Revision <SvnRevision>] [-IgnoreExternals] [-Force]
 [<CommonParameters>]
```

## DESCRIPTION
Extraire une copie de travail � partir d'un d�p�t.

## EXAMPLES

### Exemple 1 : Extraire une copie de travail

Extraire le d�p�t serf dans le r�pertoire 'serf-trunk' :

```powershell
svn-checkout https://svn.apache.org/repos/asf/serf/trunk serf-trunk
```

## PARAMETERS

### -Force
Forcer l'ex�cution de l'op�ration.

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
Indique � Subversion d'ignorer les d�finitions externes et les copies de travail externes g�r�es par elles.

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
Sp�cifie un chemin d'une copie de travail. Si le CHEMIN est omis, le nom de base de l'URL sera utilis� comme destination.

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
Sp�cifie une r�vision avec laquelle op�rer. Vous pouvez fournir des num�ros de r�vision, des mots-cl�s ou des dates (entre accolades) comme arguments � l'option de r�vision. Si vous souhaitez offrir une plage de r�visions, vous pouvez fournir deux r�visions s�par�es par un deux-points.

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
Sp�cifie une URL d'un d�p�t. Si plusieurs URL sont fournies, chacune sera extraite dans un sous-r�pertoire de CHEMIN, le nom du sous-r�pertoire �tant le nom de base de l'URL.

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
