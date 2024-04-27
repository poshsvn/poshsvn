// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using PoshSvn.Enums;

namespace PoshSvn
{
    public class SvnTarget
    {
        public string Value { get; }
        public SvnTargetType Type { get; }
        public SvnRevision Revision { get; }

        public SvnTarget(string pathOrUrl)
        {
            if (pathOrUrl.Contains("://"))
            {
                Type = SvnTargetType.Url;
                Value = pathOrUrl;
                Revision = null;
            }
            else
            {
                Type = SvnTargetType.Path;
                Value = pathOrUrl;
                Revision = null;
            }
        }

        public SvnTarget(FileSystemInfo fileInfo) :
            this(fileInfo.FullName, SvnTargetType.LiteralPath, null)
        {
        }

        public static explicit operator SvnTarget(FileSystemInfo fileInfo)
        {
            return new SvnTarget(fileInfo);
        }

        private SvnTarget(string value, SvnTargetType type, SvnRevision revision)
        {
            Value = value;
            Type = type;
            Revision = revision;
        }

        public static SvnTarget FromPath(string path)
        {
            return new SvnTarget(path, SvnTargetType.Path, null);
        }

        public static SvnTarget FromLiteralPath(string literalPath)
        {
            return new SvnTarget(literalPath, SvnTargetType.LiteralPath, null);
        }

        public static SvnTarget FromUrl(string url)
        {
            return new SvnTarget(url, SvnTargetType.Url, null);
        }
    }
}
