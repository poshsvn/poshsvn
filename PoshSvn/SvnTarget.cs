// Copyright (c) Timofei Zhakov. All rights reserved.

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

        private SvnTarget(string value, SvnTargetType type)
        {
            Value = value;
            Type = type;
        }

        public static SvnTarget FromPath(string path)
        {
            return new SvnTarget(path, SvnTargetType.Path);
        }

        public static SvnTarget FromLiteralPath(string path)
        {
            return new SvnTarget(path, SvnTargetType.LiteralPath);
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
