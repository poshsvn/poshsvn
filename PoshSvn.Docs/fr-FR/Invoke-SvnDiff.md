---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnDelete/
schema: 2.0.0
---

# Invoke-SvnDiff

## SYNOPSIS
Cela affiche les différences entre deux révisions ou chemins.

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
Affiche les différences entre deux cibles. Vous pouvez utiliser svn-diff de la manière suivante :

- Utilisez simplement svn diff pour afficher les modifications locales dans une copie de travail.
- Affichez les différences entre deux cibles, spécifiées par les paramètres `-Old` et `-New`.
  Les cibles peuvent différer par le chemin, l'URL et la révision.
  Vous pouvez trouver plus d'informations sur le système de cibles PoshSvn ici : [about_PoshSvnTarget](https://www.poshsvn.com/docs/about_PoshSvnTarget/).

## EXAMPLES

### Exemple 1

Comparer `BASE` et votre copie de travail (l'une des utilisations les plus courantes de `svn-diff`):

```powershell
svn-diff COMMITTERS.md
Index: COMMITTERS.md
===================================================================
--- COMMITTERS.md	(révision 4404)
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
--- COMMITTERS	(révision 3000)
+++ COMMITTERS	(révision 3500)
```

### Exemple 3

Ou vous pouvez utiliser un dictionnaire avec des paramètres.

```powershell
$parameters = @{
    Old = "https://svn.example.com/repos/trunk/COMMITTERS@3000"
    New = "https://svn.example.com/repos/trunk/COMMITTERS@3500"
}
svn-diff $parameters

Index: COMMITTERS
===================================================================
--- COMMITTERS	(révision 3000)
+++ COMMITTERS	(révision 3500)
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

### -Git
Utilisez le format de diff étendu de Git.

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
Ignore les propriétés pendant l'opération.

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
Spécifie la cible la plus récente.

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
Ne pas afficher les différences pour les fichiers ajoutés.

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
Ne pas afficher les différences pour les fichiers supprimés.

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
Différenciez les nœuds non liés comme une suppression et un ajout.

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
Spécifie la cible la plus récente.

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
Génère un diff adapté aux outils de correctifs tiers génériques.
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
Affiche uniquement les propriétés pendant l'opération.

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
Ne pas différencier les fichiers copiés ou déplacés avec leur source.

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
Spécifie la cible pour l'opération.

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
