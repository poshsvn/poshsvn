// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using SharpSvn;

namespace PoshSvn
{
    public class SvnNotifyOutput
    {
        public SvnNotifyAction Action { get; set; }
        public string ActionString => SvnUtils.GetActionStringShort(Action);
        public string Path { get; set; }

        public override string ToString()
        {
            return Format(ActionString, Path);
        }

        public string ToString(EngineIntrinsics context)
        {
            return Format(ActionString, PathUtils.FormatRelativePath(context, Path));
        }

        private static string Format(string actionString, string path)
        {
            return string.Format("{0,-7} {1}", actionString, path);
        }
    }
}
