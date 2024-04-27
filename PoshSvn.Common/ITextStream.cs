// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public interface ITextStream : IDisposable
    {
        void Write(char[] chars, int startIndex, int charCount);
    }
}
