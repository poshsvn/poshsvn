// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;

namespace PoshSvn.CmdLets
{

    [Cmdlet("Invoke", "SvnExport")]
    [Alias("svn-export")]
    [OutputType(typeof(SvnNotifyOutput))]
    public class SvnExportCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SvnTarget Source { get; set; }

        [Parameter(Position = 1)]
        public string Destination { get; set; } = ".";

        protected override void Execute()
        {
            SharpSvn.SvnTarget source = TargetCollection.ConvertTargetToSvnTarget(GetTarget(Source));
            string destination = GetUnresolvedProviderPathFromPSPath(Destination);

            SvnClient.Export(source, destination);
        }
    }
}
