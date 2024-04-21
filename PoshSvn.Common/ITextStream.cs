// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn.Common
{
    public interface ITextStream : IDisposable
    {
        void Write(char[] chars, int startIndex, int charCount);
    }
}
