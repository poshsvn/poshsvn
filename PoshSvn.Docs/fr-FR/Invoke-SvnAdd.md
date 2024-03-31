---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnAdd/
schema: 2.0.0
---

# Invoke-SvnAdd

## SYNOPSIS

Ajoute des fichiers, des r�pertoires ou des liens symboliques.

## SYNTAX

```
Invoke-SvnAdd [-Path] <String[]> [-Depth <SvnDepth>] [-Force] [-NoIgnore] [-NoAutoProps] [-Parents]
 [<CommonParameters>]
```

## DESCRIPTION

Planifie l'ajout de fichiers, de r�pertoires ou de liens symboliques dans votre copie de travail pour les ajouter au d�p�t. Ils seront t�l�charg�s et ajout�s au d�p�t lors de votre prochain commit.

## EXAMPLES

### Exemple 1 : Ajouter un fichier

Pour ajouter un fichier � votre copie de travail :

```powershell
svn-add .\foo.c

A       foo.c
```

### Exemple 2 : Ajouter un r�pertoire avec du contenu

Lors de l'ajout d'un r�pertoire, le comportement par d�faut de `svn-add` est de r�cursivit� :

```powershell
svn-add testdir

A       testdir
A       testdir\a
A       testdir\b
A       testdir\c
A       testdir\d
```

### Exemple 3 : Ajouter un r�pertoire sans contenu

Vous pouvez ajouter un r�pertoire sans ajouter son contenu :

```powershell
svn-add otherdir -Depth Empty

A       otherdir
```

### Exemple 4 : Ajouter un r�pertoire versionn�

Les tentatives d'ajouter un �l�ment qui est d�j� versionn� �choueront par d�faut. Pour outrepasser le comportement par d�faut et forcer Subversion � parcourir les r�pertoires d�j� versionn�s, passez l'option `-Force` :

```powershell
svn-add VersionedDirictory
Invoke-SvnAdd: 'C:\Users\cmpilato\projects\subversion\site' est d�j� sous contr�le de version.
svn-add VersionedDirictory -Force
A        VersionedDirictory\foo.c
A        VersionedDirictory\somedir\bar.c
A        VersionedDirictory\otherdir\docs\baz.doc
```

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
Default value: Recursive
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Ignore les chemins d�j� versionn�s.

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
D�sactive le param�trage automatique des propri�t�s, outrepassant la directive de configuration runtime enable-auto-props.

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
Ignore les exclusions par d�faut et les exclusions des propri�t�s svn:ignore et svn:global-ignores.

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
Ajoute les r�pertoires parents interm�diaires.

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
Sp�cifie le chemin de l'�l�ment � ajouter.

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
Ce cmdlet prend en charge les param�tres courants : -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction et -WarningVariable. Pour plus d'informations, consultez [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String[]

## OUTPUTS

### PoshSvn.SvnNotifyOutput

## NOTES

## RELATED LINKS

[svn-commit](https://www.poshsvn.com/docs/Invoke-SvnCommit/)

[svn-delete](https://www.poshsvn.com/docs/Invoke-SvnDelete/)
