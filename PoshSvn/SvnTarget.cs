// Copyright (c) Timofei Zhakov. All rights reserved.

using SharpSvn;

namespace PoshSvn
{
    public class PoshSvnTarget
    {
        public string Value { get; }
        public SvnTargetType Type { get; }

        public PoshSvnTarget(string pathOrUrl)
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

        private PoshSvnTarget(string value, SvnTargetType type)
        {
            Value = value;
            Type = type;
        }

        public static PoshSvnTarget FromPath(string path)
        {
            return new PoshSvnTarget(path, SvnTargetType.Path);
        }

        public static PoshSvnTarget FromLiteralPath(string path)
        {
            return new PoshSvnTarget(path, SvnTargetType.Path);
        }

        public static PoshSvnTarget FromUrl(string url)
        {
            return new PoshSvnTarget(url, SvnTargetType.Url);
        }
    }

    public enum SvnTargetType
    {
        Path,
        LiteralPath,
        Url
    }
}
