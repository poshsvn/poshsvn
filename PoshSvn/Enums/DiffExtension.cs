// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    [Flags]
    public enum DiffExtension
    {
        None = 0,
        Unified = 0x1,
        IgnoreSpaceChange = 0x2,
        IgnoreAllSpace = 0x4,
        Context = 0x8,
        ShowCFunction = 0x10,
    }
}
