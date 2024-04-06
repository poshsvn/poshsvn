// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn
{
    public class SvnChangeItem
    {
        public SvnChangeItem()
        {
        }

        internal SvnChangeItem(SharpSvn.SvnChangeItem source)
        {
            PropertiesModified = source.PropertiesModified;
            ContentModified = source.ContentModified;
            NodeKind = source.NodeKind.ToSharpSvnNodeKind();
            CopyFromRevision = source.CopyFromRevision;
            CopyFromPath = source.CopyFromPath;
            Action = source.Action.ToPoshSvnChangeAction();
            Path = source.Path;
        }

        public bool? PropertiesModified { get; set; }
        public bool? ContentModified { get; set; }
        public SvnNodeKind NodeKind { get; set; }
        public long? CopyFromRevision { get; set; }
        public string CopyFromPath { get; set; }
        public SvnChangeAction Action { get; set; }
        public string ActionString => SvnUtils.GetChangeActionString(Action);
        public string Path { get; set; }
    }
}
