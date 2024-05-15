// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public static class SvnRevisionUtils
    {
        public static SvnRevisionRange WorkingChangesRange = new SvnRevisionRange(
            new SvnRevision(SvnRevisionType.Base),
            new SvnRevision(SvnRevisionType.Working));

        public static SvnRevisionRangeBase CreateRangeFromRevisionOrChange(SvnRevisionRange revisionRange,
                                                                           SvnRevisionChange revisionChange,
                                                                           SvnRevisionRange revisionDefault)
        {
            // Throw if -r and -c are specified.
            if (revisionRange != null && revisionChange != null)
            {
                throw new ArgumentException("Multiple revision arguments encountered; can't specify -c twice, or both -c and -r.");
            }

            // -r is specified
            if (revisionRange != null)
            {
                return revisionRange;
            }

            // -c is specified
            if (revisionChange != null)
            {
                return revisionChange;
            }

            // No revision range is specified, so return default.
            return revisionDefault;
        }
    }
}
