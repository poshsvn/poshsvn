// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Collections.Generic;

namespace PoshSvn
{
    public static class DiffExtensionExtensions
    {
        public static IEnumerable<string> ConvertToArgumentCollection(this DiffExtension diffExtensions)
        {
            if (diffExtensions.HasFlag(DiffExtension.Unified))
            {
                yield return "-u";
            }

            if (diffExtensions.HasFlag(DiffExtension.IgnoreSpaceChange))
            {
                yield return "-b";
            }

            if (diffExtensions.HasFlag(DiffExtension.IgnoreAllSpace))
            {
                yield return "-w";
            }

            if (diffExtensions.HasFlag(DiffExtension.Context))
            {
                yield return "-U";
            }

            if (diffExtensions.HasFlag(DiffExtension.ShowCFunction))
            {
                yield return "-p";
            }
        }
    }
}
