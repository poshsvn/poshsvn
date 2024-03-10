// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public enum SvnDepth
    {
        Empty,
        Files,
        Immediates,
        Infinity
    }

    public static class SvnDepthExtensions
    {
        public static SharpSvn.SvnDepth ConvertToSharpSvnDepth(this SvnDepth depth)
        {
            switch (depth)
            {
                case SvnDepth.Empty:
                    return SharpSvn.SvnDepth.Empty;
                case SvnDepth.Files:
                    return SharpSvn.SvnDepth.Files;
                case SvnDepth.Immediates:
                    return SharpSvn.SvnDepth.Children;
                case SvnDepth.Infinity:
                    return SharpSvn.SvnDepth.Infinity;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
