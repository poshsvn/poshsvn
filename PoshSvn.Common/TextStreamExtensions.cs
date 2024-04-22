// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn.Common
{
    public static class TextStreamExtensions
    {
        public static void Write(this ITextStream output, string str)
        {
            char[] chars = str.ToCharArray();
            output.Write(chars, 0, chars.Length);
        }
    }
}
