// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public class SvnBlameLine
    {
        public long Revision { get; set; }
        public string Author { get; set; }
        public string Line { get; set; }

        public SharpSvn.SvnPropertyValue[] MergedRevisionProperties { get; set; }
        public SharpSvn.SvnPropertyValue[] RevisionProperties { get; set; }
        public bool LocalChange { get; set; }
        public long MergedRevision { get; set; } = -1;
        public DateTime MergedTime { get; set; }
        public string MergedPath { get; set; }
        public string MergedAuthor { get; set; }
        public DateTime Time { get; set; }
        public long LineNumber { get; set; }
        public long EndRevision { get; set; }
        public long StartRevision { get; set; }
    }
}