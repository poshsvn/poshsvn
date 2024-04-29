// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnRelocate")]
    [Alias("svn-relocate")]
    public class SvnRelocateCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Uri From { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Uri To { get; set; }

        [Parameter(Position = 2)]
        public string Path { get; set; } = ".";

        [Parameter()]
        public SwitchParameter IgnoreExternals;

        protected override void Execute()
        {
            string path = GetUnresolvedProviderPathFromPSPath(Path);

            var args = new SvnRelocateArgs
            {
                IgnoreExternals = IgnoreExternals,
            };

            SvnClient.Relocate(path, From, To, args);
        }
    }
}
