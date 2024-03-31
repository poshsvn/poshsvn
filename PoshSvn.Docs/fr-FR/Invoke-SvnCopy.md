---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnCopy
schema: 2.0.0
---

# Invoke-SvnCopy

## SYNOPSIS
Copier des fichiers et des r�pertoires dans une copie de travail ou un d�p�t.

## SYNTAX

### Local (Default)
```
Invoke-SvnCopy [-Source] <SvnTarget[]> [-Destination] <SvnTarget> [-Revision <SvnRevision>] [-IgnoreExternals]
 [-Parents] [<CommonParameters>]
```

### Remote
```
Invoke-SvnCopy [-Source] <SvnTarget[]> [-Destination] <SvnTarget> -Message <String> [-Revision <SvnRevision>]
 [-IgnoreExternals] [-Parents] [<CommonParameters>]
```

## DESCRIPTION
La source et la destination peuvent chacune �tre soit un chemin de copie de travail (WC) soit une URL :
- De `WC` � `WC` : copier et planifier l'ajout (avec historique)
- De `WC` � `Url` : envoyer imm�diatement une copie de WC vers Url
- De `Url` � `WC` : v�rifier Url dans WC, planifier l'ajout
- De `Url` � `Url` : copie compl�te c�t� serveur ; utilis�e pour cr�er des branches et des tags

Toutes les sources doivent �tre du m�me type. Si la destination est un r�pertoire existant, les sources seront ajout�es comme enfants de la destination. Lors de la copie de plusieurs sources, la destination doit �tre un r�pertoire existant.

## EXAMPLES

### Exemple 1
```powershell
svn-copy foo.txt bar.txt

A       bar.txt

svn-status

Statut  Chemin
------  ------
A  +    b.txt
```

Cette commande copie un �l�ment dans votre copie de travail (cela planifie la copie - rien n'est envoy� dans le d�p�t avant que vous ne validiez).

### Exemple 2
```powershell
svn-copy -Source bat.c, baz.c, qux.c -Destination src

A        src/bat.c
A        src/baz.c
A        src/qux.c
```

Cette commande copie plusieurs fichiers dans une copie de travail dans un r�pertoire.

### Exemple 3
```powershell
svn-copy -Revision 8 bat.c ya-old-bat.c

A        ya-old-bat.c
```

Cette commande copie la r�vision 8 de `bat.c` dans votre copie de travail sous un nom diff�rent.

### Exemple 4
```powershell
svn-copy -Source near.txt -Destination https://svn.example.com/repos/far-away.txt -Message "Copie distante."

R�vision valid�e 8.
```

Cette commande copie un �l�ment de votre copie de travail vers une URL dans le d�p�t (c'est une validation imm�diate, donc vous devez fournir un message de validation).

### Exemple 5
```powershell
svn-copy https://svn.example.com/repos/far-away .\near-here -Revision 6

A         near-here
```

Cette commande copie un �l�ment du d�p�t vers votre copie de travail (cela planifie simplement la copie - rien n'est envoy� dans le d�p�t avant que vous ne validiez).

Astuce : C'est la mani�re recommand�e de ressusciter un fichier perdu dans votre d�p�t !

### Exemple 6

```powershell
$src = https://svn.example.com/repos/far-away
$dst = https://svn.example.com/repos/over-there

svn-copy -Source $src -Destination $dst -Message "Copie distante."

R�vision valid�e 9.
```

Cette commande effectue une copie entre deux URL distantes. Vous pouvez utiliser des variables pour simplifier votre commande.

### Exemple 7

```powershell
$root = "https://svn.example.com/repos/test"
svn-copy -Source "$root/trunk" -DestinationUrl "$root/tags/0.6.32-prerelease" -Message "Cr�ation de la branche"

R�vision valid�e 12.
```

Cette commande cr�e une �tiquette dans un d�p�t. Vous pouvez utiliser le param�tre `-DestinationUrl` pour sp�cifier qu'il s'agit d'une op�ration distante.

## PARAMETERS

### -Destination
{{ Fill Destination Description }}

```yaml
Type: SvnTarget
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IgnoreExternals
{{ Fill IgnoreExternals Description }}

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

### -Message
{{ Fill Message Description }}

```yaml
Type: String
Parameter Sets: Remote
Aliases: m

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Parents
{{ Fill Parents Description }}

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

### -Revision
{{ Fill Revision Description }}

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

### -Source
{{ Fill Source Description }}

```yaml
Type: SvnTarget[]
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

### PoshSvn.SvnNotifyOutput

### PoshSvn.CmdLets.SvnCommitOutput

## NOTES

## RELATED LINKS
