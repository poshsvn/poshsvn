---
external help file: PoshSvn.dll-Help.xml
Module Name: PoshSvn
online version: https://www.poshsvn.com/docs/Invoke-SvnAdminCreate/
schema: 2.0.0
---

# Invoke-SvnCat

## SYNOPSIS

Obtient le contenu de l'�l�ment au chemin, � l'URL et � la r�vision sp�cifi�s.

## SYNTAX

```
Invoke-SvnCat [[-Target] <SvnTarget>] [-AsByteStream] [-Raw] [<CommonParameters>]
```

## DESCRIPTION

Le cmdlet `svn-cat` r�cup�re le contenu de l'�l�ment au chemin, � l'URL et � la r�vision sp�cifi�s. Pour les fichiers, le contenu est lu ligne par ligne et renvoie une collection d'objets, chacun repr�sentant une ligne de contenu.

## EXAMPLES

### Exemple 1

Si vous souhaitez visualiser le fichier README.md dans votre d�p�t sans le v�rifier :

```powershell
svn-cat https://svn.rinrab.com/rinrab/poshsvn/trunk/README.md

# Client Apache Subversion pour PowerShell

## Fonctionnalit�s

- Compl�tion automatique (expansion de tabulation) des commandes et des param�tres.
- Sortie typ�e des cmdlets.
- Sortie format�e pour reproduire l'exp�rience utilisateur de l'interface de ligne de commande Subversion.

## Installation

Installez le module PoshSvn depuis la galerie PowerShell :

``powershell
Install-Module -Name PoshSvn
``
```

### Exemple 2

Vous pouvez �galement visualiser des versions sp�cifiques de fichiers.

````powershell
svn-cat https://svn.rinrab.com/rinrab/poshsvn/trunk/README.md -Revision 489

# Client Apache Subversion pour PowerShell

## Fonctionnalit�s

- Compl�tion automatique (expansion de tabulation) des commandes et des param�tres.
- Sortie typ�e des cmdlets.
...

````

## PARAMETERS

### -AsByteStream
Sp�cifie que le contenu doit �tre lu comme un flux d'octets.

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

### -Raw
Ignore les caract�res de saut de ligne et renvoie l'int�gralit� du contenu d'un fichier dans une seule cha�ne avec les sauts de ligne pr�serv�s. Par d�faut, les caract�res de saut de ligne dans un fichier sont utilis�s comme d�limiteurs pour s�parer l'entr�e en un tableau de cha�nes.

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
Sp�cifie la cible sur laquelle op�rer.

```yaml
Type: SvnTarget
Parameter Sets: (All)
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

### None

## OUTPUTS

### System.String

### System.Byte

## NOTES

## RELATED LINKS

[Invoke-SvnList](https://www.poshsvn.com/docs/Invoke-SvnList/)

[Get-Content](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.management/get-content)
