// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using SharpSvn;

namespace PoshSvn
{
    public class SvnTarget
    {
        public string Value { get; }
        public SvnTargetType Type { get; }

        public SvnTarget(string pathOrUrl)
        {
            if (pathOrUrl.Contains("://") && SvnUriTarget.TryParse(pathOrUrl, true, out _))
            {
                Type = SvnTargetType.Url;
            }
            else
            {
                Type = SvnTargetType.Path;
            }

            Value = pathOrUrl;
        }

        public SvnTarget(FileSystemInfo fileInfo)
        {
            Type = SvnTargetType.LiteralPath;
            Value = fileInfo.FullName;
        }

        public static explicit operator SvnTarget(FileSystemInfo fileInfo)
        {
            return new SvnTarget(fileInfo);
        }

        private SvnTarget(string value, SvnTargetType type)
        {
            Value = value;
            Type = type;
        }

        public static SvnTarget FromPath(string path)
        {
            return new SvnTarget(path, SvnTargetType.Path);
        }

        public static SvnTarget FromLiteralPath(string literalPath)
        {
            return new SvnTarget(literalPath, SvnTargetType.LiteralPath);
        }

        public static SvnTarget FromUrl(string url)
        {
            return new SvnTarget(url, SvnTargetType.Url);
        }
    }

    public enum SvnTargetType
    {
        Path,
        LiteralPath,
        Url
    }
}
