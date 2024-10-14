# Changelog

All notable changes to this project will be documented in this file.

# [1.1.2, 1.1.3]

- Update Subversion from 1.14.3 to 1.14.4
- Update APR from 1.7.4 to 1.7.5
- Update Expat from 2.5.0 to 2.6.3
- Update ZLIB from 1.3.0 to 1.3.1

# [1.1.1]

- Build improvements
  - Build EXPAT and ZLIB libraries as static.
  - Use SQLite amalgamation.
  - Other small build fixes and improvements.
- Little installer improvements.
  - Make 'Add Subversion to PATH' as separate feature.
  - Drop 'PoshSvn offline docs' feature, because it wasn't supported.

# [1.1.0]

- Added `-Change` parameter to all cmdlets.
- Added `-AsByteStream`, `-Raw`, and `-Encoding` parameters to the `svn-diff` cmdlet.
- Implement the svn-upgrade cmdlet.
- Minor fixes and improvement, specialy for the `svn-diff` and `svn-log` cmdlets.
- Build improvements.

## [1.0.1]

- Fixed help build.
- Updated docs.

## [1.0.0]

- Added `-Recursive` parameter to cmdlets that has `-Depth` parameter.
- Added all parameters to the `svn-commit` cmdlet.
- Remove SharpSvn.SvnPropertyCollection from output of cmdlets.
- Added `-Encoding` parameter to the `svn-cat` and `svn-diff` cmdlet.
- Properties to the `svn-cleanup` cmdlet.
- Added custom `Format-List` formatter for the `svn-blame` cmdlet.
- Ignore `StatusCompleted` notify action; write it only to verbose.
- Added short aliases to cmdlets.
- Do not write action to verbose if it is already written to output.
- Added -TrustServerCertificateFailures parameter to all cmdlets.
- Fix progress title (replace 'Processing' with correct titile).
- Removed svn-dist from release.
- [VSCode extension] Add `Alt+U` hotkey to open poshsvn terminal.
- [VSCode extension] Do not create new terminal if it is already exists.
- [VSCode extension] Fix `Ctrl+Backspace` and other shortcuts in terminal.
- [VSCode extension] Added demo screenshot.
- Minor fixes and improvement.
- Build improvements.

## [0.7.4]

- Fix name of svn-dist in release.

## [0.7.3]

- Fix upload of svn-dist to release.

## [0.7.2]

- Upload svn-dist to release.
- Enable SSPI in serf.

## [0.7.1]

- Build and publish improvements.

## [0.7.0]

- Added properties cmdlets.
  - Implement `svn-propdel` cmdlet.
  - Implement `svn-propget` cmdlet.
  - Implement `svn-proplist` cmdlet.
  - Implement `svn-propset` cmdlet.
  - Functions works over rvision properties
  - Added formatters for SvnProperty object.
- Implement `svn-mergeinfo` cmdlet.
- Implement `svn-relocate` cmdlet.
- Some fixes with targets of cmdlets.
- Fix peg-revision.
- Fix version of the MSI installer.
- Minor fixes and improvements.
- Build and publish improvements.

## [0.6.2]

- Build and publish improvements.

## [0.6.1]

- Add build for x86 platform.
- Build and publish improvements.

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
