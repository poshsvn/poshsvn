// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public static class SvnAcceptExtensions
    {
        public static SharpSvn.SvnAccept ToSharpSvnAccept(this SvnAccept accept)
        {
            switch (accept)
            {
                case SvnAccept.Prompt:
                    throw new ArgumentException("'Propmt' is not supported by SharpSvn.", nameof(accept));
                case SvnAccept.Postpone:
                    return SharpSvn.SvnAccept.Postpone;
                case SvnAccept.Base:
                    return SharpSvn.SvnAccept.Base;
                case SvnAccept.TheirsFull:
                    return SharpSvn.SvnAccept.TheirsFull;
                case SvnAccept.MineFull:
                    return SharpSvn.SvnAccept.MineFull;
                case SvnAccept.Theirs:
                    return SharpSvn.SvnAccept.Theirs;
                case SvnAccept.Mine:
                    return SharpSvn.SvnAccept.Mine;
                case SvnAccept.Merged:
                    return SharpSvn.SvnAccept.Merged;
                case SvnAccept.Unspecified:
                    return SharpSvn.SvnAccept.Unspecified;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
