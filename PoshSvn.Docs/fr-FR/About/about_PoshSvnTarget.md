---
description: Describes how to specify cmdlet target.
Locale: en-US
online version: https://www.poshsvn.com/docs/about_PoshSvnTarget/
schema: 2.0.0
title: About PoshSvn Cmdlet Target
---

# PoshSvn
## about_PoshSvnTarget

## Short description

Description de la façon de spécifier la cible d'une cmdlet.

## Long description

Certaines opérations Subversion prennent en charge la spécification d'un chemin local vers une copie de travail ou d'une URL distante vers un dépôt en tant que cible.

Vous pouvez spécifier la cible de l'opération en définissant le paramètre `-Target` sur soit un chemin ou une URL. Cependant, si vous préférez indiquer directement s'il s'agit d'une URL ou d'un chemin, utilisez la cmdlet New-SvnTarget et spécifiez la cible via les paramètres `-Url` ou `-Path` respectivement.

### Spécification de la cible via le paramètre `-Target`

Cette méthode de spécification de la cible est optimale pour une utilisation de base de Subversion. Vous pouvez passer soit un chemin ou une URL au paramètre `-Target`, que PoshSvn interprétera en conséquence.

Les exemples suivants récupèrent `svn-info` depuis le dépôt distant et la copie de travail :

```powershell
svn-info -Target https://svn.apache.org/repos/asf/serf/trunk
svn-info -Target .\chemin\vers\wc
```

Cependant, vous n'avez pas besoin d'écrire `-Target` avant de spécifier la cible, car il accepte la valeur des arguments restants. C'est pourquoi cela répète presque le comportement de l'interface en ligne de commande de Subversion :

```powershell
svn-info https://svn.apache.org/repos/asf/serf/trunk
svn-info .\chemin\vers\wc
```

Dans certaines cmdlets, si aucune cible n'est spécifiée, PoshSvn utilisera par défaut le répertoire actuel. Cela signifie que l'exemple suivant récupérera `svn-info` à propos de la copie de travail située à `C:\chemin\vers\wc` :

```
cd C:\chemin\vers\wc
svn-info
```

### Spécification de la cible en indiquant directement son type

Si vous écrivez un script, vous pouvez spécifier explicitement si la cible est une URL ou un chemin. Cette approche est optimale pour les scripts car elle garantit la lisibilité, la compréhension et aide à éviter les erreurs causées par une détection incorrecte du type de cible :

```powershell
svn-info (New-SvnTarget -Url https://svn.apache.org/repos/asf/serf/trunk)
svn-info (New-SvnTarget -Path .\chemin\vers\wc)
```

Si vous préférez, vous pouvez utiliser une variable pour la cible de la cmdlet. Par exemple :

```powershell
# Créer la cible
$cible = (New-SvnTarget -Url https://svn.apache.org/repos/asf/serf/trunk)

# Afficher la cible
Write-Output $cible

# Appeler svn-info
svn-info -Target $cible
```

Si une cible avec un type différent est spécifiée, cela entraînera une erreur :

```powershell
svn-info (New-SvnTarget -Path https://svn.apache.org/repos/asf/serf/trunk)
# Erreur
svn-info (New-SvnTarget -Url .\chemin\vers\wc)
# Erreur
```

De plus, vous pouvez utiliser la variable [`$PSScriptRoot`](https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_automatic_variables?view=powershell-7.4#psscriptroot) si votre copie de travail est située près du script. Cette variable automatique fournit le chemin vers le répertoire contenant le script.

```powershell
svn-info (New-SvnTarget -Path $PSScriptRoot\chemin\vers\wc)
```
