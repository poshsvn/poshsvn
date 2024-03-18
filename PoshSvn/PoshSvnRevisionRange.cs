// Copyright (c) Timofei Zhakov. All rights reserved.

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
    }
}
