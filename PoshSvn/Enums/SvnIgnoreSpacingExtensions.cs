// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public static class SvnIgnoreSpacingExtensions
    {
        public static SharpSvn.SvnIgnoreSpacing ToSharpSvnIgnoreSpacing(this SvnIgnoreSpacing ignoreSpacing)
        {
            switch (ignoreSpacing)
            {
                case SvnIgnoreSpacing.None:
                    return SharpSvn.SvnIgnoreSpacing.None;

                case SvnIgnoreSpacing.IgnoreSpace:
                    return SharpSvn.SvnIgnoreSpacing.IgnoreSpace;

                case SvnIgnoreSpacing.IgnoreAll:
                    return SharpSvn.SvnIgnoreSpacing.IgnoreAll;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
