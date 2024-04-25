# Contributing to PoshSvn

## I Want To Contribute

> ### Legal Notice 
> When contributing to this project, you must agree that you have
> authored 100% of the content, that you have the necessary rights
> to the content and that the content you contribute may be provided
> under the project license.

### Reporting Bug, Suggesting Enhancements, and Asking Questions

All bug reports and enhancement suggestions are welcome!
You could create an issue at [GitHub](https://github.com/rinrab/poshsvn/issues/new).
If it is bug report, please add more additional information,
if possible.

### Code contributions

#### Getting the source code

The master repository of PoshSvn project uses Subversion
as source control, however the source code is avalible
at official [GitHub mirror](https://github.com/rinrab/poshsvn).
You might chckout the master repository by the following link:
`https://svn.rinrab.com/rinrab/poshsvn/trunk` with `guest`
username and empty password.

#### Building PoshSvn

To build PoshSvn you are required to have Visual Studio installed
on your machine with the following components:

- Desktop development with C++
  - vcpkg package manager
- .NET desktop development

The only you need to compile PoshSvn is to open the `PoshSvn.sln`
solution in Visual Studio and press `F7`. Then the build process
will start and automaticaly build PoshSvn with all its components.

#### Code review

There is no prefered way to review code for now. All GitHub
Pull requests would be reviewed and then merged via patch
or something else to the master repository.

#### Becoming a committer

If you want to become a committer of the PoshSvn project, let me know.
There is also no procedure for this (for now). Also you have to provide
me the following information:

- Username: the Subversion author name for account to be created.
- email and Full name: used for GitHub mirror. Must be same as at GitHub.

#### Log messages

- Log messages should exaplain changes.
- Do not commit unrelated changes.
