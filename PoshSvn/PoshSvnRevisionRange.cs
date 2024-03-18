// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Collections.Generic;

namespace PoshSvn
{
    public class PoshSvnRevisionRange
    {
        public PoshSvnRevision EndRevision { get; set; }
        public PoshSvnRevision StartRevision { get; set; }

        public PoshSvnRevisionRange(PoshSvnRevision endRevision, PoshSvnRevision startRevision)
        {
            EndRevision = endRevision;
            StartRevision = startRevision;
        }

        public override bool Equals(object obj)
        {
            return obj is PoshSvnRevisionRange range &&
                   EqualityComparer<PoshSvnRevision>.Default.Equals(EndRevision, range.EndRevision) &&
                   EqualityComparer<PoshSvnRevision>.Default.Equals(StartRevision, range.StartRevision);
        }
    }
}
