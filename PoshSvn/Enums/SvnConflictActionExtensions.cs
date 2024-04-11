// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn
{
    public static class SvnConflictActionExtensions
    {
        public static SvnConflictAction ToPoshSvnConflictActions(this SharpSvn.SvnConflictAction action)
        {
            return (SvnConflictAction)action;
        }

        public static SharpSvn.SvnConflictAction ToSharpSvnConflictActions(this SvnConflictAction action)
        {
            return (SharpSvn.SvnConflictAction)action;
        }
    }
}
