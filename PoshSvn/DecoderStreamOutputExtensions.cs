// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn
{
    public static class DecoderStreamOutputExtensions
    {
        public static void Write(this IDecoderStreamOutput output, string str)
        {
            char[] chars = str.ToCharArray();
            output.Write(chars, 0, chars.Length);
        }
    }
}
