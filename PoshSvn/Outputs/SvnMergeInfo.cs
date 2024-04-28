// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public class SvnMergeInfo
    {
        public bool IsReintegration { get; set; }
        public Uri RepositoryRootUrl { get; set; }
        public Uri YoungestCommonAncestorUrl { get; set; }
        public long YoungestCommonAncestorRevision { get; set; }
        public Uri BaseUrl { get; set; }
        public long BaseRevision { get; set; }
        public Uri RightUrl { get; set; }
        public long RightRevision { get; set; }
        public Uri TargetUrl { get; set; }
        public long TargetRevision { get; set; }
    }
}
