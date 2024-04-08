// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn
{
    public static class SvnNotifyActionExtensions
    {
        // The PoshSvn and SharpSvn SvnNotifyAction enums has the same layout.

        public static SharpSvn.SvnNotifyAction ToSharpSvnNotifyAction(this SvnNotifyAction svnNotifyAction)
        {
            return (SharpSvn.SvnNotifyAction)svnNotifyAction;
        }

        public static SvnNotifyAction ToPoshSvnNotifyAction(this SharpSvn.SvnNotifyAction svnNotifyAction)
        {
            return (SvnNotifyAction)svnNotifyAction;
        }
    }
}
