# Contributing to PoshSvn

## I Want To Contribute

> ### Legal Notice 
> When contributing to this project, you must agree that you have
> authored 100% of the content, that you have the necessary rights
> to the content and that the content you contribute may be provided
> under the project license.

### Reporting Bug, Suggesting Enhancements, and Asking Questions

All bug reports and enhancement suggestions are welcome!
You can create an issue on [GitHub](https://github.com/rinrab/poshsvn/issues/new).
If it's a bug report, please provide additional information
if possible.

### Code contributions

#### Getting the source code

The master repository of the PoshSvn project uses Subversion
as source control; however, the source code is available
on the official [GitHub mirror](https://github.com/rinrab/poshsvn).
You can checkout the master repository using the following link:
`https://svn.rinrab.com/rinrab/poshsvn/trunk` with `guest`
as the username and an empty password.

#### Building PoshSvn

To build PoshSvn, you are required to have Visual Studio installed
on your machine with the following components:

- Desktop development with C++
  - vcpkg package manager
- .NET desktop development

All you need to compile PoshSvn is to open the `PoshSvn.sln`
solution in Visual Studio and press `F7`. The build process
will start automatically and build PoshSvn with all its components.

#### Code review

There is no preferred way to review code for now. All GitHub
Pull Requests will be reviewed and then merged via patch
or another method to the master repository.

#### Becoming a committer

If you want to become a committer of the PoshSvn project, let me know.
There is also no procedure for this (for now). Also, you have to provide
me with the following information:

- Username: The Subversion author name for the account to be created.
- Email and Full Name: Used for the GitHub mirror. Must be the same as on GitHub.

#### Log messages

- Log messages should explain changes.
- Do not commit unrelated changes.
