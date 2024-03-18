// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.Generic;

namespace PoshSvn
{
    public class PoshSvnRevisionRange
    {
        public PoshSvnRevision EndRevision { get; set; }
        public PoshSvnRevision StartRevision { get; set; }

        public PoshSvnRevisionRange(string str) 
        {
            string[] tokens = str.Split(new char[] { ':' });

            if (tokens.Length == 1)
            {
                PoshSvnRevision revision = new PoshSvnRevision(tokens[0]);
                StartRevision = revision;
                EndRevision = revision;
            }
            else if (tokens.Length == 2)
            {
                StartRevision = new PoshSvnRevision(tokens[0]);
                EndRevision = new PoshSvnRevision(tokens[1]);
            }
            else
            {
                throw new ArgumentException("Please specify correct revision range.", "Revision");
            }
        }

        public PoshSvnRevisionRange(PoshSvnRevision startRevision, PoshSvnRevision endRevision)
        {
            StartRevision = startRevision;
            EndRevision = endRevision;
        }

        public PoshSvnRevisionRange(long startRevision, long endRevision)
        {
            StartRevision = new PoshSvnRevision(startRevision);
            EndRevision = new PoshSvnRevision(endRevision);
        }

        public override bool Equals(object obj)
        {
            return obj is PoshSvnRevisionRange range &&
                   EqualityComparer<PoshSvnRevision>.Default.Equals(EndRevision, range.EndRevision) &&
                   EqualityComparer<PoshSvnRevision>.Default.Equals(StartRevision, range.StartRevision);
        }
    }
}
