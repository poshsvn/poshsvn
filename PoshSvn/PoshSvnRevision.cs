// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

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
            int i = 0;

            while (i < str.Length && (str[i] == 'r' || str[i] == ' '))
            {
                i++;
            }

            if (i < str.Length && str[i] == '{')
            {
                throw new NotImplementedException(); // TODO:
            }
            else if (long.TryParse(str.Substring(i), out long revisionNumber))
            {
                Revision = revisionNumber;
                RevisionType = PoshSvnRevisionType.Number;
            }
            else
            {
                string word = str.Substring(i).Trim();

                if (word.Equals("head", StringComparison.CurrentCultureIgnoreCase))
                {
                    RevisionType = PoshSvnRevisionType.Head;
                }
                else if (word.Equals("prev", StringComparison.CurrentCultureIgnoreCase))
                {
                    RevisionType = PoshSvnRevisionType.Previous;
                }
                else if (word.Equals("base", StringComparison.CurrentCultureIgnoreCase))
                {
                    RevisionType = PoshSvnRevisionType.Base;
                }
                else if (word.Equals("committed", StringComparison.CurrentCultureIgnoreCase))
                {
                    RevisionType = PoshSvnRevisionType.Committed;
                }
                else
                {
                    throw new ArgumentException("Cannot parse revision.");
                }
            }
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

        public override bool Equals(object obj)
        {
            return obj is PoshSvnRevision revision &&
                   RevisionType == revision.RevisionType &&
                   Revision == revision.Revision;
        }
    }
}
