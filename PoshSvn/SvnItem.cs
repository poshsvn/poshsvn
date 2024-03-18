// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using SharpSvn;

namespace PoshSvn
{
    public class SvnItem
    {
        public string Path { get; set; }

        public SvnNodeKind NodeKind { get; set; }
        public DateTime Date { get; set; }

        public Uri Uri { get; set; }
        public string ExternalTarget { get; set; }
        public Uri ExternalParent { get; set; }
        public Uri BaseUri { get; set; }
        public Uri RepositoryRoot { get; set; }
        public string Name { get; set; }
        public string BasePath { get; set; }
    }

    public class SvnItemDetailed : SvnItem
    {
        public string Author { get; set; }
        public long Revision { get; set; }
        public long? FileSize { get; set; }
        public bool HasProperties { get; set; }
    }
}
