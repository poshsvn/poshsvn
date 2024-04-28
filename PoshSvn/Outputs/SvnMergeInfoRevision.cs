// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public class SvnMergeInfoRevision
    {
        public long Revision { get; set; }
        public string LogMessage { get; set; }
        public Uri SourceUri { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
    }
}
