---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnAdd/
schema: 2.0.0
---

# Invoke-SvnAdd

## SYNOPSIS

Ajoute des fichiers, des répertoires ou des liens symboliques.

## SYNTAX

```
Invoke-SvnAdd [-Path] <String[]> [-Depth <SvnDepth>] [-Force] [-NoIgnore] [-NoAutoProps] [-Parents]
 [<CommonParameters>]
```

## DESCRIPTION

Planifie l'ajout de fichiers, de répertoires ou de liens symboliques dans votre copie de travail pour les ajouter au dépôt. Ils seront téléchargés et ajoutés au dépôt lors de votre prochain commit.

## EXAMPLES

### Exemple 1 : Ajouter un fichier

Pour ajouter un fichier à votre copie de travail :

```powershell
svn-add .\foo.c

A       foo.c
```

### Exemple 2 : Ajouter un répertoire avec du contenu

Lors de l'ajout d'un répertoire, le comportement par défaut de `svn-add` est de récursivité :

```powershell
svn-add testdir

A       testdir
A       testdir\a
A       testdir\b
A       testdir\c
A       testdir\d
```

### Exemple 3 : Ajouter un répertoire sans contenu

Vous pouvez ajouter un répertoire sans ajouter son contenu :

```powershell
svn-add otherdir -Depth Empty

A       otherdir
```

### Exemple 4 : Ajouter un répertoire versionné

Les tentatives d'ajouter un élément qui est déjà versionné échoueront par défaut. Pour outrepasser le comportement par défaut et forcer Subversion à parcourir les répertoires déjà versionnés, passez l'option `-Force` :

```powershell
svn-add VersionedDirictory
Invoke-SvnAdd: 'C:\Users\cmpilato\projects\subversion\site' est déjà sous contrôle de version.
svn-add VersionedDirictory -Force
A        VersionedDirictory\foo.c
A        VersionedDirictory\somedir\bar.c
A        VersionedDirictory\otherdir\docs\baz.doc
```

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
Default value: Recursive
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Ignore les chemins déjà versionnés.

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

### -NoAutoProps
Désactive le paramétrage automatique des propriétés, outrepassant la directive de configuration runtime enable-auto-props.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: no-auto-props

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NoIgnore
Ignore les exclusions par défaut et les exclusions des propriétés svn:ignore et svn:global-ignores.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: no-ignore

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Parents
Ajoute les répertoires parents intermédiaires.

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

### -Path
Spécifie le chemin de l'élément à ajouter.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
Ce cmdlet prend en charge les paramètres courants : -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction et -WarningVariable. Pour plus d'informations, consultez [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### PoshSvn.SvnNotifyOutput

## NOTES

## RELATED LINKS

[svn-commit](https://www.poshsvn.com/docs/Invoke-SvnCommit/)

[svn-delete](https://www.poshsvn.com/docs/Invoke-SvnDelete/)
