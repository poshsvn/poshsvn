// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;

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

        protected override void Execute()
        {
            string path = GetUnresolvedProviderPathFromPSPath(Path);

            SvnClient.Relocate(path, From, To);
        }
    }
}
