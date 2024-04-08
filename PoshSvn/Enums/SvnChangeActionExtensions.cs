// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public static class SvnChangeActionExtensions
    {
        public static SvnChangeAction ToPoshSvnChangeAction(this SharpSvn.SvnChangeAction changeAction)
        {
            switch (changeAction)
            {
                case SharpSvn.SvnChangeAction.None: return SvnChangeAction.None;
                case SharpSvn.SvnChangeAction.Add: return SvnChangeAction.Add;
                case SharpSvn.SvnChangeAction.Delete: return SvnChangeAction.Delete;
                case SharpSvn.SvnChangeAction.Modify: return SvnChangeAction.Modify;
                case SharpSvn.SvnChangeAction.Replace: return SvnChangeAction.Replace;
                default: throw new NotImplementedException();
            }
        }
    }
}
