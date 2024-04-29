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

        public static SvnRevisionType ToPoshSvnRevisionType(this SharpSvn.SvnRevisionType revisionType)
        {
            switch (revisionType)
            {
                case SharpSvn.SvnRevisionType.None:
                    return SvnRevisionType.None;

                case SharpSvn.SvnRevisionType.Number:
                    return SvnRevisionType.Number;

                case SharpSvn.SvnRevisionType.Time:
                    return SvnRevisionType.Time;

                case SharpSvn.SvnRevisionType.Committed:
                    return SvnRevisionType.Committed;

                case SharpSvn.SvnRevisionType.Previous:
                    return SvnRevisionType.Previous;

                case SharpSvn.SvnRevisionType.Base:
                    return SvnRevisionType.Base;

                case SharpSvn.SvnRevisionType.Working:
                    return SvnRevisionType.Working;

                case SharpSvn.SvnRevisionType.Head:
                    return SvnRevisionType.Head;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
