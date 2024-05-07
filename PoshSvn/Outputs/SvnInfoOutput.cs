// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using SharpSvn;

namespace PoshSvn
{
    public class SvnInfoOutput
    {
        public string Path { get; set; }
        public Uri Url { get; set; }
        public Uri RelativeUrl { get; set; }
        public Uri RepositoryRoot { get; set; }
        public Guid RepositoryId { get; set; }
        public long Revision { get; set; }
        public SvnNodeKind NodeKind { get; set; }
        public string LastChangedAuthor { get; set; }
        public long LastChangedRevision { get; set; }
        public DateTimeOffset LastChangedDate { get; set; }

        public SvnSchedule? Schedule { get; set; } = null;
        public string WorkingCopyRoot { get; set; } = null;

        public DateTimeOffset? TextLastUpdated { get; set; } = null;
        public string Checksum { get; set; } = null;
    }
}
