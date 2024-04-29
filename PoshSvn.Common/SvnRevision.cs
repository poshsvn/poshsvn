// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public class SvnRevision
    {
        public SvnRevisionType RevisionType { get; set; }
        public long Revision { get; set; }

        public SvnRevision(long revision)
        {
            RevisionType = SvnRevisionType.Number;
            Revision = revision;
        }

        public SvnRevision(string str)
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
                RevisionType = SvnRevisionType.Number;
            }
            else
            {
                string word = str.Substring(i).Trim();

                if (word.Equals("head", StringComparison.CurrentCultureIgnoreCase))
                {
                    RevisionType = SvnRevisionType.Head;
                }
                else if (word.Equals("prev", StringComparison.CurrentCultureIgnoreCase))
                {
                    RevisionType = SvnRevisionType.Previous;
                }
                else if (word.Equals("base", StringComparison.CurrentCultureIgnoreCase))
                {
                    RevisionType = SvnRevisionType.Base;
                }
                else if (word.Equals("committed", StringComparison.CurrentCultureIgnoreCase))
                {
                    RevisionType = SvnRevisionType.Committed;
                }
                else
                {
                    throw new ArgumentException("Cannot parse revision.");
                }
            }
        }

        public SvnRevision(SvnRevisionType type)
        {
            RevisionType = type;
        }

        public override bool Equals(object obj)
        {
            return obj is SvnRevision revision &&
                   RevisionType == revision.RevisionType &&
                   Revision == revision.Revision;
        }
    }
}
