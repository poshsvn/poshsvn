// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public class SvnLogOutput
    {
        public long Revision { get; set; }
        public string Author { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string Message { get; set; }
        public SvnChangeItem[] ChangedPaths { get; set; }
        public SvnProperty[] RevisionProperties { get; set; }
    }
}
