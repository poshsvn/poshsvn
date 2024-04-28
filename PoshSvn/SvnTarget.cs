// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using PoshSvn.Enums;

namespace PoshSvn
{
    public class SvnTarget
    {
        public string Value { get; set; }
        public SvnTargetType Type { get; set; }
        public SvnRevision Revision { get; set; }

        public SvnTarget(string pathOrUrl)
        {
            PegRevision.ParsePegRevisionTarget(pathOrUrl, out string remainingTarget, out SvnRevision revision);

            if (remainingTarget.Contains("://"))
            {
                Type = SvnTargetType.Url;
                Value = remainingTarget;
                Revision = revision;
            }
            else
            {
                Type = SvnTargetType.Path;
                Value = remainingTarget;
                Revision = revision;
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

        public static SvnTarget FromPath(string path, SvnRevision revision = null)
        {
            return new SvnTarget(path, SvnTargetType.Path, revision);
        }

        public static SvnTarget FromLiteralPath(string literalPath, SvnRevision revision = null)
        {
            return new SvnTarget(literalPath, SvnTargetType.LiteralPath, revision);
        }

        public static SvnTarget FromUrl(string url, SvnRevision revision = null)
        {
            return new SvnTarget(url, SvnTargetType.Url, revision);
        }
    }
}
