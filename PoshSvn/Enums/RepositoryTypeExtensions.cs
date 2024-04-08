// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using SharpSvn;

namespace PoshSvn
{
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
