// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.Generic;

namespace PoshSvn
{
    public class SvnRevisionRange
    {
        public SvnRevision EndRevision { get; set; }
        public SvnRevision StartRevision { get; set; }

        public SvnRevisionRange(string str)
        {
            string[] tokens = str.Split(new char[] { ':' });

            if (tokens.Length == 1)
            {
                SvnRevision revision = new SvnRevision(tokens[0]);
                StartRevision = revision;
                EndRevision = revision;
            }
            else if (tokens.Length == 2)
            {
                StartRevision = new SvnRevision(tokens[0]);
                EndRevision = new SvnRevision(tokens[1]);
            }
            else
            {
                throw new ArgumentException("Please specify correct revision range.", "Revision");
            }
        }

        public SvnRevisionRange(SvnRevision startRevision, SvnRevision endRevision)
        {
            StartRevision = startRevision;
            EndRevision = endRevision;
        }

        public SvnRevisionRange(long startRevision, long endRevision)
        {
            StartRevision = new SvnRevision(startRevision);
            EndRevision = new SvnRevision(endRevision);
        }

        public SvnRevisionRange(SvnRevisionType start, SvnRevisionType end)
        {
            StartRevision = new SvnRevision(start);
            EndRevision = new SvnRevision(end);
        }

        public override bool Equals(object obj)
        {
            return obj is SvnRevisionRange range &&
                   EqualityComparer<SvnRevision>.Default.Equals(EndRevision, range.EndRevision) &&
                   EqualityComparer<SvnRevision>.Default.Equals(StartRevision, range.StartRevision);
        }
    }
}
