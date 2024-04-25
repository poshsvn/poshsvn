# Writing PoshSvn code

This document describes PoshSvn project structure and hacking instructions.

## Project structure

- www/ - PoshSvn website
- Assets/ - PoshSvn images and logos.
- bin/ - Output directory with binaries. Generate while build.
- Crescendo/ - Oldest version of PoshSvn; only autocompleate for Subversion command line, done via Crescendo utitlity. This project is no longer supported.
- Installer/ - MSI installer of PoshSvn and Subversion binaries.
- MSBuild/ - MSBuild utilities.
- obj/ Intermidiate directory. Generate while build.
- PoshSvn/ - PoshSvn core with cmdlets, types, and enums.
- PoshSvn.Common/ - Common healpers and utilities.
- PoshSvn.Common.Tests/ - Tests on PoshSvn.Common project.
- PoshSvn.Docs/ - PoshSvn documentation.
- PoshSvn.Package/ - PoshSvn poweshell package; builds PoshSvn into package and ZIP.
- PoshSvn.Static/ - Static files of PoshSvn; includes mdoule declaration file and custom formats.
- PoshSvn.Tests/ - Tests on PoshSvn cmdlets.
- PoshSvn.VSCode/ - PoshSvn VSCode extension.
- Scripts/ - Utility scripts for PoshSvn
- SharpSvn/ - Fork of SharpSvn project with some modifications for PoshSvn.
- SvnDist/ - Project, which builds Subversion binaries into ZIP package.
- svn-po/ - Subversion localization files, needed for SharpSvn localization.
- vcpkg/ - Project, for building vcpkg dependencies.
- vcpkg_installed/ - Installed dependencies; produced at buillding of vcpkg project.

## Code style

https://github.com/rinrab/poshsvn/blob/trunk/.editorconfig
