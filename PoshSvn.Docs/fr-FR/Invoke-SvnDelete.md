---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnDelete/
schema: 2.0.0
---

# Invoke-SvnDelete

## SYNOPSIS
Supprimer des fichiers et des r�pertoires du contr�le de version.

## SYNTAX

### Local (Default)
```
Invoke-SvnDelete [-Target] <SvnTarget[]> [-Force] [-KeepLocal] [<CommonParameters>]
```

### Remote
```
Invoke-SvnDelete [-Target] <SvnTarget[]> [-Message <String>] [-Force] [-KeepLocal] [<CommonParameters>]
```

## DESCRIPTION
Supprime des �l�ments locaux ou distants de la version.

Chaque �l�ment sp�cifi� par un chemin est planifi� pour la suppression lors de la prochaine validation.

Chaque �l�ment sp�cifi� par une URL est supprim� du d�p�t via une validation imm�diate.

## EXAMPLES

### Exemple 1
```powershell
svn-delete foo.c

D       foo.c
```

Supprime et planifie la suppression du fichier foo.c.

### Exemple 2
```powershell
svn-delete https://svn.example.com/repos/foo.c -Message "supprimer foo.c"

R�vision valid�e 57.
```

Cette commande supprime le fichier situ� � `https://svn.example.com/repos/foo.c` et valide la suppression avec le message de journal "supprimer foo.c".

## PARAMETERS

### -Force
Force l'op�ration � s'ex�cuter.

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

### -KeepLocal
Conserve l'�l�ment dans la copie de travail.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: keep-local

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Message
Sp�cifie le message de journal.

```yaml
Type: String
Parameter Sets: Remote
Aliases: m

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Target
Sp�cifie le chemin ou l'URL de l'�l�ment � supprimer.

```yaml
Type: SvnTarget[]
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
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
