---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnLog/
schema: 2.0.0
---

# Invoke-SvnLog

## SYNOPSIS
Affiche les messages de journal pour un ensemble de r�vision(s) et/ou de chemin(s).

## SYNTAX

```
Invoke-SvnLog [[-Target] <SvnTarget[]>] [-Revision <PoshSvnRevisionRange[]>] [-ChangedPaths] [-Limit <Int32>]
 [-Depth <SvnDepth>] [-IncludeExternals] [-WithAllRevisionProperties] [-WithNoRevisionProperties]
 [-WithRevisionProperties <String[]>] [<CommonParameters>]
```

## DESCRIPTION
Affiche les messages de journal du d�p�t. Si aucune cible n'est fournie, `svn-log` affiche les messages de journal pour tous les fichiers et r�pertoires � l'int�rieur du r�pertoire de travail actuel de votre copie de travail.

Vous pouvez fournir une cha�ne avec une URL ou un chemin au param�tre de cible ou les sp�cifier manuellement aux param�tres `-Url` ou `-Path`.

## EXAMPLES

### Example 1
```powershell
svn-log

------------------------------------------------------------------------
r20        harry               2003-01-17 22:56 -06:00

Ajustement.
------------------------------------------------------------------------
r17        sally               2003-01-16 23:21 -06:00
...
```

Cette commande affiche les messages de journal du r�pertoire actuel et de tous les �l�ments � l'int�rieur.

### Example 2
```powershell
svn-log foo.c

------------------------------------------------------------------------
r32        sally               2003-01-13 00:43 -06:00

Ajout des d�finitions.
------------------------------------------------------------------------
r28        sally               2003-01-07 21:48 -06:00
...
```

Cette commande affiche les messages de journal du fichier sp�cifique, dans cet exemple `foo.c`.

### Example 3
```powershell
svn-log https://svn.example.com/repos/foo.c

------------------------------------------------------------------------
r32        sally               2003-01-13 00:43 -06:00

Ajout des d�finitions.
------------------------------------------------------------------------
r28        sally               2003-01-07 21:48 -06:00
...
```

Cette commande affiche les messages de journal de l'�l�ment distant par URL.

### Example 4
```powershell
svn-log https://svn.example.com/repos/foo.c https://svn.example.com/repos/bar.c

------------------------------------------------------------------------
r32        sally               2003-01-13 00:43 -06:00

Ajout des d�finitions.
------------------------------------------------------------------------
r31        harry               2003-01-10 12:25 -06:00

Ajout du nouveau fichier bar.c
------------------------------------------------------------------------
r28        sally               2003-01-07 21:48 -06:00
...
```

Cette commande affiche les messages de journal de `foo.c` et `bar.c` � l'int�rieur du r�f�rentiel `https://svn.example.com/repos`.

Important : les �l�ments doivent �tre dans le m�me r�f�rentiel.

### Example 5
```powershell
svn-log | Format-Table

  R�vision Auteur           Date                   Message                                                              
  -------- ------           ----                   -------                                                              
        32 sally            2003-01-13    00:43    Ajout des d�finitions.
        31 harry            2003-01-10    12:25    Ajout du nouveau fichier bar.c
        28 sally            2003-01-07    21:48    ...
```

Cette commande affiche les messages de journal et les formate en tableau. Vous pouvez l'utiliser pour obtenir une sortie plus compacte.

## PARAMETERS

### -ChangedPaths
{{ Fill ChangedPaths Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: v

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

### -IncludeExternals
Tells Subversion to include externals definitions and the external working copies managed by them.

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

### -Limit
Specifies the maximum number of log entries.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases: l

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Revision
{{ Fill Revision Description }}

```yaml
Type: PoshSvnRevisionRange[]
Parameter Sets: (All)
Aliases: rev, r

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Target
Specifies the Path or the Url of an item to show log. Path and Url targets cannot be combined in one invokation.

```yaml
Type: SvnTarget[]
Parameter Sets: (All)
Aliases:

Required: False
Position: 0
Default value: .
Accept pipeline input: False
Accept wildcard characters: False
```

### -WithAllRevisionProperties
Retrieve all revision properties.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: with-all-revprops, WithAllRevprops

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WithNoRevisionProperties
Retrieve no revision properties.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: with-no-revprops, WithNoRevprops

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WithRevisionProperties
Retrieve specified revision properties.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases: with-revprop

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

### PoshSvn.CmdLets.SvnLogOutput

## NOTES

## RELATED LINKS
