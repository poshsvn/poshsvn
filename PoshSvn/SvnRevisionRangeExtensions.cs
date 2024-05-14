// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn
{
    public static class SvnRevisionRangeExtensions
    {
        public static SharpSvn.SvnRevisionRange ToSharpSvnRevisionRange(this SvnRevisionRangeBase revisionRange)
        {
            return new SharpSvn.SvnRevisionRange(
                revisionRange.StartRevision.ToSharpSvnRevision(),
                revisionRange.EndRevision.ToSharpSvnRevision());
        }
    }
}
