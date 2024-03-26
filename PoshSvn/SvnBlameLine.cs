// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn
{
    public class SvnBlameLine
    {
        public long Revision { get; set; }
        public string Author { get; set; }
        public long LineNumber { get; set; }
        public string Line { get; set; }
    }
}