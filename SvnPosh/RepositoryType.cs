using SharpSvn;
using System;

namespace SvnPosh
{
    public enum RepositoryType
    {
        FsFs,

        // BerkeleyDB,
        // TODO: doesn't work. Throws the following exception:
        //   Subversion filesystem driver for Berkeley DB (SharpSvn-DB44-20-x64.dll) is not installed. Can't access this repository kind.

        // Fsx,
        // TODO: does not supported by SharpSvn
    }

    public static class RepositoryTypeExtensions
    {
        public static SvnRepositoryFileSystem ConvertToSvnRepositoryFileSystem(this RepositoryType repositoryType)
        {
            switch (repositoryType)
            {
                case RepositoryType.FsFs:
                    return SvnRepositoryFileSystem.FsFs;
                //case RepositoryType.BerkeleyDB:
                //    return SvnRepositoryFileSystem.BerkeleyDB;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
