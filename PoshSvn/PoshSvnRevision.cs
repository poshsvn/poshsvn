// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn
{
    public class PoshSvnRevision
    {
        public PoshSvnRevisionType RevisionType { get; set; }
        public long Revision { get; set; }

        public PoshSvnRevision(long revision)
        {
            RevisionType = PoshSvnRevisionType.Number;
            Revision = revision;
        }

        public PoshSvnRevision(string str)
        {
            // TODO:
        }

        public PoshSvnRevision(PoshSvnRevisionType type)
        {
            RevisionType = type;
        }

        public SharpSvn.SvnRevision ToSharpSvnRevision()
        {
            if (RevisionType == PoshSvnRevisionType.Number)
            {
                return new SharpSvn.SvnRevision(Revision);
            }
            else
            {
                return new SharpSvn.SvnRevision(RevisionType.ToSharpSvnRevisionType());
            }
        }
    }
}
