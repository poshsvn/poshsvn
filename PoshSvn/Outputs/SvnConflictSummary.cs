// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn
{
    public class SvnConflictSummary
    {
        public string FileName { get; set; }
        public SvnConflictAction Action { get; set; }
    }
}
