// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using System.Text;
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
            ParsePegRevisionTarget(pathOrUrl, out string remainingTarget, out SvnRevision revision);

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

        public static void ParsePegRevisionTarget(string target, out string remainingTarget, out SvnRevision revision)
        {
            int i = 0;

            StringBuilder remainingTargetSb = new StringBuilder();
            for (; i < target.Length; i++)
            {
                if (target[i] == '@')
                {
                    i++; // Skip '@'
                    break;
                }
                else
                {
                    remainingTargetSb.Append(target[i]);
                }
            }
            remainingTarget = remainingTargetSb.ToString();

            if (i < target.Length)
            {
                // We have revision

                StringBuilder revisionSb = new StringBuilder();

                for (; i < target.Length; i++)
                {
                    revisionSb.Append(target[i]);
                }

                revision = new SvnRevision(revisionSb.ToString());
            }
            else
            {
                // We don't have any more symbols, so peg-revision is not set.

                revision = null;
            }
        }
    }
}
