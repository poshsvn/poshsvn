// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnImport")]
    [Alias("svn-import")]
    [OutputType(typeof(SvnNotifyOutput))]
    public class SvnImportCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0)]
        public string Source { get; set; } = ".";

        [Parameter(Position = 1, Mandatory = true)]
        public Uri Destination { get; set; }

        [Parameter(Mandatory = true)]
        [Alias("m")]
        public string Message { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Infinity;

        protected override void Execute()
        {
            SharpSvn.SvnImportArgs args = new SharpSvn.SvnImportArgs
            {
                Depth = Depth.ConvertToSharpSvnDepth(),
                LogMessage = Message
            };

            SvnClient.RemoteImport(GetUnresolvedProviderPathFromPSPath(Source), Destination, args);
        }
    }
}
