// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public static class SvnRevisionTypeExtensions
    {
        public static SharpSvn.SvnRevisionType ToSharpSvnRevisionType(this PoshSvnRevisionType revisionType)
        {
            switch (revisionType)
            {
                case PoshSvnRevisionType.None:
                    return SharpSvn.SvnRevisionType.None;

                case PoshSvnRevisionType.Number:
                    return SharpSvn.SvnRevisionType.Number;

                case PoshSvnRevisionType.Time:
                    return SharpSvn.SvnRevisionType.Time;

                case PoshSvnRevisionType.Committed:
                    return SharpSvn.SvnRevisionType.Committed;

                case PoshSvnRevisionType.Previous:
                    return SharpSvn.SvnRevisionType.Previous;

                case PoshSvnRevisionType.Base:
                    return SharpSvn.SvnRevisionType.Base;

                case PoshSvnRevisionType.Working:
                    return SharpSvn.SvnRevisionType.Working;

                case PoshSvnRevisionType.Head:
                    return SharpSvn.SvnRevisionType.Head;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
