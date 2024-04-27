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

        public static PoshSvnRevisionType ToPoshSvnRevisionType(this SharpSvn.SvnRevisionType revisionType)
        {
            switch (revisionType)
            {
                case SharpSvn.SvnRevisionType.None:
                    return PoshSvnRevisionType.None;

                case SharpSvn.SvnRevisionType.Number:
                    return PoshSvnRevisionType.Number;

                case SharpSvn.SvnRevisionType.Time:
                    return PoshSvnRevisionType.Time;

                case SharpSvn.SvnRevisionType.Committed:
                    return PoshSvnRevisionType.Committed;

                case SharpSvn.SvnRevisionType.Previous:
                    return PoshSvnRevisionType.Previous;

                case SharpSvn.SvnRevisionType.Base:
                    return PoshSvnRevisionType.Base;

                case SharpSvn.SvnRevisionType.Working:
                    return PoshSvnRevisionType.Working;

                case SharpSvn.SvnRevisionType.Head:
                    return PoshSvnRevisionType.Head;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
