// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class SvnCommonParameterAttribute : Attribute
    {
    }
}
