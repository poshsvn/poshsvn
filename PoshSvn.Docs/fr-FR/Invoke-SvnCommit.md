---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnCommit/
schema: 2.0.0
---

# Invoke-SvnCommit

## SYNOPSIS
Envoyer les modifications de votre copie de travail vers le d�p�t.

## SYNTAX

```
Invoke-SvnCommit [[-Path] <String[]>] -Message <String> [-RevisionProperties <Hashtable>] [<CommonParameters>]
```

## DESCRIPTION
Envoyez les modifications de votre copie de travail vers le d�p�t.

svn-commit enverra tout jeton de verrouillage qu'il trouve et lib�rera les verrous sur tous les CHEMINS commis (de mani�re r�cursive) sauf si -NoUnlock est pass�.

## EXAMPLES

### Exemple 1

Valider une simple modification d'un fichier :

```powershell
svn-commit -Message "ajout de la section howto."

M        a.txt
R�vision confirm�e 3.
```

### Exemple 2

Valider un fichier programm� pour la suppression :

```powershell
svn-commit -Message "suppression du fichier 'c'."

D        c.txt
R�vision confirm�e 7.
```

### Exemple 3

Par d�faut, la cible de la commande fait r�f�rence � votre r�pertoire actuel.
Vous pouvez �galement la sp�cifier en d�finissant le param�tre `-Path` sur un chemin de votre choix :

```powershell
svn-commit -Path C:\chemin\vers\wc -Message "ajout de la section howto."

M        a.txt
R�vision confirm�e 3.
```

## PARAMETERS

### -Message
Indique que vous sp�cifierez soit un message de journal, soit un commentaire de verrouillage sur la ligne de commande, en suivant cette option. Par exemple : `svn-commit -Message "Ils ne font pas le dimanche."`

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Sp�cifie le chemin de l'�l�ment � valider. Par d�faut, c'est `.` (r�pertoire actuel).

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

### -RevisionProperties
Sp�cifie la propri�t� de r�vision dans la nouvelle r�vision.

```yaml
Type: Hashtable
Parameter Sets: (All)
Aliases: with-revprop, rp, revprop

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

### PoshSvn.CmdLets.SvnCommitOutput

## NOTES

## RELATED LINKS
