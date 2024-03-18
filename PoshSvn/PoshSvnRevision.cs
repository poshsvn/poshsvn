// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn
{
    public class PoshSvnRevision
    {
        public SvnRevisionType RevisionType { get; set; }
        public long Revision { get; set; }

        public PoshSvnRevision(long revision)
        {
            RevisionType = SvnRevisionType.Number;
            Revision = revision;
        }

        public PoshSvnRevision(string str)
        {
            // TODO:
        }

        public PoshSvnRevision(SvnRevisionType type)
        {
            RevisionType = type;
        }

        public SharpSvn.SvnRevision ToSharpSvnRevision()
        {
            if (RevisionType == SvnRevisionType.Number)
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
