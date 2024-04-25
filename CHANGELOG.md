# Changelog

All notable changes to this project will be documented in this file.

## [0.6.1]

- Add build for x86 platform.

## [0.6.0]

- Implement `svn-lock` cmdlet.
- Implement `svn-unlock` cmdlet.
- Implement `svn-merge` cmdlet.
- Compile SharpSvn myself instead of using its NuGet package.
  - All Subversion binaries are now in theirs DLLs (originaly SharpSvn compiles them into one DLL).
  - Many other improvements.
  - Use vcpkg to build dependecies.
- Add French-localized documentation.
- Add MSI installer for PoshSvn and Subversion command-line tools.
- Minor fixes and improvement.

## [0.5.0]

- Implement `svn-cat` cmdlet.
- Implement `svn-diff` cmdlet.
- Implement `svn-blame` cmdlet.
- Some fixes in commit output.
- Minor fixes and improvement.

## [0.4.0]

- Added vscode extension.
- Minor fixes and improvement.

## [0.3.0]

- Add `-Revision` parameter to `svn-log` cmdlet.
- Implement `svn-import` and `svn-export` cmdlets.
- Minor fixes and improvement.

## [0.2.0]

- Rework targets of cmdlets.
- Implement `svn-switch`.
- Minor fixes and improvement.

## [0.1.3]

- Minor fixes and improvement.
- Implement `svn-copy`.

## [0.1.2]

- Add documentation.
- Some fixes in metadata.

## [0.1.1]

- Include CRT to package
- Minor fixes in module manifest

## [0.1.0]

- Initial release
