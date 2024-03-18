// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public static class SvnRevisionTypeExtensions
    {
        public static SharpSvn.SvnRevisionType ToSharpSvnRevisionType(this SvnRevisionType revisionType)
        {
            switch (revisionType)
            {
                case SvnRevisionType.None:
                    return SharpSvn.SvnRevisionType.None;

                case SvnRevisionType.Number:
                    return SharpSvn.SvnRevisionType.Number;

                case SvnRevisionType.Time:
                    return SharpSvn.SvnRevisionType.Time;

                case SvnRevisionType.Committed:
                    return SharpSvn.SvnRevisionType.Committed;

                case SvnRevisionType.Previous:
                    return SharpSvn.SvnRevisionType.Previous;

                case SvnRevisionType.Base:
                    return SharpSvn.SvnRevisionType.Base;

                case SvnRevisionType.Working:
                    return SharpSvn.SvnRevisionType.Working;

                case SvnRevisionType.Head:
                    return SharpSvn.SvnRevisionType.Head;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
