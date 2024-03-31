---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnDelete/
schema: 2.0.0
---

# Invoke-SvnDiff

## SYNOPSIS
Cela affiche les diff�rences entre deux r�visions ou chemins.

## SYNTAX

### Target (Default)
```
Invoke-SvnDiff [[-Target] <SvnTarget[]>] [-Depth <SvnDepth>] [-NoDiffAdded] [-NoDiffDeleted]
 [-IgnoreProperties] [-PropertiesOnly] [-ShowCopiesAsAdds] [-NoticeAncestry] [-Changelist <String>] [-Git]
 [-PatchCompatible] [<CommonParameters>]
```

### TwoFiles
```
Invoke-SvnDiff [-Old <SvnTarget>] [-New <SvnTarget>] [-Depth <SvnDepth>] [-NoDiffAdded] [-NoDiffDeleted]
 [-IgnoreProperties] [-PropertiesOnly] [-ShowCopiesAsAdds] [-NoticeAncestry] [-Changelist <String>] [-Git]
 [-PatchCompatible] [<CommonParameters>]
```

## DESCRIPTION
Affiche les diff�rences entre deux cibles. Vous pouvez utiliser svn-diff de la mani�re suivante :

- Utilisez simplement svn diff pour afficher les modifications locales dans une copie de travail.
- Affichez les diff�rences entre deux cibles, sp�cifi�es par les param�tres `-Old` et `-New`.
  Les cibles peuvent diff�rer par le chemin, l'URL et la r�vision.
  Vous pouvez trouver plus d'informations sur le syst�me de cibles PoshSvn ici : [about_PoshSvnTarget](https://www.poshsvn.com/docs/about_PoshSvnTarget/).

## EXAMPLES

### Exemple 1

Comparer `BASE` et votre copie de travail (l'une des utilisations les plus courantes de `svn-diff`):

```powershell
svn-diff COMMITTERS.md
Index: COMMITTERS.md
===================================================================
--- COMMITTERS.md	(r�vision 4404)
+++ COMMITTERS.md	(copie de travail)
...
```

### Exemple 2

Comparer `BASE` et votre copie de travail (l'une des utilisations les plus courantes de `svn-diff`):

```powershell
svn-diff -Old https://svn.example.com/repos/trunk/COMMITTERS@3000 `
         -New https://svn.example.com/repos/trunk/COMMITTERS@3000

Index: COMMITTERS
===================================================================
--- COMMITTERS	(r�vision 3000)
+++ COMMITTERS	(r�vision 3500)
```

### Exemple 3

Ou vous pouvez utiliser un dictionnaire avec des param�tres.

```powershell
$parameters = @{
    Old = "https://svn.example.com/repos/trunk/COMMITTERS@3000"
    New = "https://svn.example.com/repos/trunk/COMMITTERS@3500"
}
svn-diff $parameters

Index: COMMITTERS
===================================================================
--- COMMITTERS	(r�vision 3000)
+++ COMMITTERS	(r�vision 3500)
```

## PARAMETERS

### -Changelist
Specifies a changelist to operate.

```yaml
Type: String
Parameter Sets: (All)
Aliases: cl

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Git
Utilisez le format de diff �tendu de Git.

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

### -IgnoreProperties
Ignore les propri�t�s pendant l'op�ration.

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

### -New
Sp�cifie la cible la plus r�cente.

```yaml
Type: SvnTarget
Parameter Sets: TwoFiles
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoDiffAdded
Ne pas afficher les diff�rences pour les fichiers ajout�s.

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

### -NoDiffDeleted
Ne pas afficher les diff�rences pour les fichiers supprim�s.

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

### -NoticeAncestry
Diff�renciez les n�uds non li�s comme une suppression et un ajout.

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

### -Old
Sp�cifie la cible la plus r�cente.

```yaml
Type: SvnTarget
Parameter Sets: TwoFiles
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PatchCompatible
G�n�re un diff adapt� aux outils de correctifs tiers g�n�riques.
Currently the same as `-ShowCopiesAsAdds` `-IgnoreProperties`.

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

### -PropertiesOnly
Affiche uniquement les propri�t�s pendant l'op�ration.

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

### -ShowCopiesAsAdds
Ne pas diff�rencier les fichiers copi�s ou d�plac�s avec leur source.

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
Sp�cifie la cible pour l'op�ration.

```yaml
Type: SvnTarget[]
Parameter Sets: Target
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

### PoshSvn.SvnTarget[]

## OUTPUTS

### System.String

## NOTES

## RELATED LINKS
