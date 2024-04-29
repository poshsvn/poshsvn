// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public static class SvnNoneKindExtensions
    {
        public static SvnNodeKind ToPoshSvnNodeKind(this SharpSvn.SvnNodeKind nodeKind)
        {
            switch (nodeKind)
            {
                case SharpSvn.SvnNodeKind.None:
                    return SvnNodeKind.None;

                case SharpSvn.SvnNodeKind.File:
                    return SvnNodeKind.File;

                case SharpSvn.SvnNodeKind.Directory:
                    return SvnNodeKind.Directory;

                case SharpSvn.SvnNodeKind.Unknown:
                    return SvnNodeKind.Unknown;

                default:
                    throw new NotImplementedException();
            }
        }

        public static SharpSvn.SvnNodeKind ToSharpSvnNodeKind(this SvnNodeKind nodeKind)
        {
            switch (nodeKind)
            {
                case SvnNodeKind.None:
                    return SharpSvn.SvnNodeKind.None;

                case SvnNodeKind.File:
                    return SharpSvn.SvnNodeKind.File;

                case SvnNodeKind.Directory:
                    return SharpSvn.SvnNodeKind.Directory;

                case SvnNodeKind.Unknown:
                    return SharpSvn.SvnNodeKind.Unknown;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
