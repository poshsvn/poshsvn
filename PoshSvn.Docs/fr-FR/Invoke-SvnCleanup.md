---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnCleanup/
schema: 2.0.0
---

# Invoke-SvnCleanup

## SYNOPSIS
Nettoyer une copie de travail.

## SYNTAX

```
Invoke-SvnCleanup [[-Path] <String[]>] [-RemoveUnversioned] [-RemoveIgnored] [-VacuumPristines]
 [-IncludeExternals] [<CommonParameters>]
```

## DESCRIPTION

1. Lorsqu'aucune des options `-RemoveUnversioned`, `-RemoveIgnored`, et `-VacuumPristines` n'est sp�cifi�e, la suppression des verrous supprime tous les verrous d'�criture de la copie de travail.

2. Si l'option `-RemoveUnversioned` ou l'option `-RemoveIgnored` est sp�cifi�e, supprimez tous les �l�ments non versionn�s ou ignor�s.

3. Si l'option `-VacuumPristines` est sp�cifi�e, supprimez les copies originales des fichiers qui sont stock�es � l'int�rieur du r�pertoire `.svn` et qui ne sont plus r�f�renc�es par aucun fichier dans la copie de travail.

## EXAMPLES

### Exemple 1 : Nettoyer une copie de travail

Nettoyez la copie de travail situ�e � `C:\chemin\vers\wc`. En g�n�ral, cela n'est n�cessaire que si un client Subversion a plant� lors de l'utilisation de la copie de travail, la laissant dans un �tat inutilisable :

```powershell
svn-cleanup C:\chemin\vers\wc
```

## PARAMETERS

### -IncludeExternals
Op�rer �galement sur les externes d�finis par les propri�t�s `svn:externals`.

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

### -Path
Chemin d'une copie de travail � nettoyer.

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

### -RemoveIgnored
Supprimer les �l�ments ignor�s.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: remove-ignored

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RemoveUnversioned
Supprimer les �l�ments non versionn�s.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: remove-unversioned

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -VacuumPristines
Supprimer les copies originales non r�f�renc�es du r�pertoire `.svn`.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: vacuum-pristines

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
