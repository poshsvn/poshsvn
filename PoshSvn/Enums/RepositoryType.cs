// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn
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
}
