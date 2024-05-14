---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnCommit/
schema: 2.0.0
---

# Invoke-SvnCommit

## SYNOPSIS
Envoyer les modifications de votre copie de travail vers le dépôt.

## SYNTAX

```
Invoke-SvnCommit [[-Path] <String[]>] -Message <String> [-ChangeList <String[]>] [-KeepChangeLists] [-NoUnlock]
 [-Depth <SvnDepth>] [-RevisionProperties <Hashtable>] [<CommonParameters>]
```

## DESCRIPTION
Envoyez les modifications de votre copie de travail vers le dépôt.

svn-commit enverra tout jeton de verrouillage qu'il trouve et libérera les verrous sur tous les CHEMINS commis (de manière récursive) sauf si -NoUnlock est passé.

## EXAMPLES

### Exemple 1

Valider une simple modification d'un fichier :

```powershell
svn-commit -Message "ajout de la section howto."

M        a.txt
Révision confirmée 3.
```

### Exemple 2

Valider un fichier programmé pour la suppression :

```powershell
svn-commit -Message "suppression du fichier 'c'."

D        c.txt
Révision confirmée 7.
```

### Exemple 3

Par défaut, la cible de la commande fait référence à votre répertoire actuel.
Vous pouvez également la spécifier en définissant le paramètre `-Path` sur un chemin de votre choix :

```powershell
svn-commit -Path C:\chemin\vers\wc -Message "ajout de la section howto."

M        a.txt
Révision confirmée 3.
```

## PARAMETERS

### -ChangeList
{{ Fill ChangeList Description }}

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: cl

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Depth
{{ Fill Depth Description }}

```yaml
Type: SvnDepth
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -KeepChangeLists
{{ Fill KeepChangeLists Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Message
Indique que vous spécifierez soit un message de journal, soit un commentaire de verrouillage sur la ligne de commande, en suivant cette option. Par exemple : `svn-commit -Message "Ils ne font pas le dimanche."`

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

### -NoUnlock
{{ Fill NoUnlock Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Path
Spécifie le chemin de l'élément à valider. Par défaut, c'est `.` (répertoire actuel).

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
Spécifie la propriété de révision dans la nouvelle révision.

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
