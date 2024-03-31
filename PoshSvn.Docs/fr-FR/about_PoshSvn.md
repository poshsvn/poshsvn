---
description: Describes basics of PoshSvn usage.
Locale: en-US
online version: https://www.poshsvn.com/docs/about_PoshSvn/
schema: 2.0.0
title: About PoshSvn
---

# PoshSvn
## about_PoshSvn

## Short description

Décrit les bases de l'utilisation de PoshSvn.

## Long description

PoshSvn offre les fonctionnalités suivantes :

- Complétion automatique des onglets (expansion des onglets) pour les commandes et les paramètres.
- Sortie typée des cmdlets.
- Sortie formatée pour reproduire l'expérience utilisateur de l'interface en ligne de commande de Subversion.

## Concept

Le concept de PoshSvn était de reproduire l'expérience utilisateur de l'interface en ligne de commande de Subversion, y compris les paramètres, la sortie et d'autres comportements.

## Examples

### Exemple 1

La commande suivante vérifie le dépôt en utilisant l'interface en ligne de commande de Subverison :

```shell
svn checkout https://svn.apache.org/repos/asf/serf/trunk serf-trunk
A    serf-trunk\test
A    serf-trunk\test\MockHTTPinC
A    serf-trunk\test\certs
A    serf-trunk\test\certs\private
A    serf-trunk\test\certs\serfserver_san_nocn_cert.pem
...
 U   serf-trunk
Checked out revision 1916201.
```

Alors que PoshSvn nécessite :

```powershell
svn-checkout https://svn.apache.org/repos/asf/serf/trunk serf-trunk

A       serf-trunk\test
A       serf-trunk\test\MockHTTPinC
A       serf-trunk\test\certs
A       serf-trunk\test\certs\private
A       serf-trunk\test\certs\serfserver_san_nocn_cert.pem
...
U       serf-trunk
Checked out revision 1916201.
```

Vous pouvez également écrire les noms des paramètres (meilleur pour le scripting) :

```powershell
svn-checkout -Url https://svn.apache.org/repos/asf/serf/trunk -Path serf-trunk
```

Comme vous pouvez le voir, il y a très peu de différences entre l'interface en ligne de commande de Subversion et les cmdlets de PoshSvn, mais PoshSvn ajoute également certaines fonctionnalités de PowerShell telles que la progression, la sortie typée, et autres.
