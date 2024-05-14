// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Collections.Generic;

namespace PoshSvn
{
    public abstract class SvnRevisionRangeBase
    {
        public SvnRevision EndRevision { get; set; }
        public SvnRevision StartRevision { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SvnRevisionRange range &&
                   EqualityComparer<SvnRevision>.Default.Equals(EndRevision, range.EndRevision) &&
                   EqualityComparer<SvnRevision>.Default.Equals(StartRevision, range.StartRevision);
        }
    }
}
