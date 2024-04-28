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

        [Parameter()]
        [Alias("rev", "r")]
        public SvnRevision Revision { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Infinity;

        [Parameter()]
        [Alias("f")]
        public SwitchParameter Force { get; set; }

        [Parameter()]
        [Alias("ignore-externals")]
        public SwitchParameter IgnoreExternals { get; set; }

        [Parameter()]
        [Alias("ignore-keywords")]
        public SwitchParameter IgnoreKeywords { get; set; }

        protected override void Execute()
        {
            SharpSvn.SvnExportArgs args = new SharpSvn.SvnExportArgs
            {
                Depth = Depth.ConvertToSharpSvnDepth(),
                Overwrite = Force,
                IgnoreExternals = IgnoreExternals,
                IgnoreKeywords = IgnoreKeywords
            };

            if (Revision != null)
            {
                args.Revision = Revision.ToSharpSvnRevision();
            }

            SvnResolvedTarget resolvedSource = ResolveTarget(Source);
            SharpSvn.SvnTarget source = resolvedSource.ConvertToSharpSvnTarget();

            string destination = GetUnresolvedProviderPathFromPSPath(Destination);

            SvnClient.Export(source, destination, args);
        }
    }
}
