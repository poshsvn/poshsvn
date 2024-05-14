// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SvnCommonParameterAttribute : Attribute
    {
    }
}
