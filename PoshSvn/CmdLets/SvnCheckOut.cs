// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Linq;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnCheckout")]
    [Alias("svn-checkout", "svn-co")]
    [OutputType(typeof(SvnCheckoutOutput))]
    public class SvnCheckout : SvnClientCmdletBase
    {
        [Parameter(Mandatory = true, Position = 0)]
        public Uri Url { get; set; }

        [Parameter(Position = 1)]
        public string Path { get; set; }

        [Parameter()]
        [Alias("rev")]
        public SharpSvn.SvnRevision Revision { get; set; }

        [Parameter()]
        [Alias("ignore-externals")]
        public SwitchParameter IgnoreExternals { get; set; }

        [Parameter()]
        [Alias("f")]
        public SwitchParameter Force { get; set; }

        protected override void Execute()
        {
            var args = new SvnCheckOutArgs
            {
                Revision = Revision,
                IgnoreExternals = IgnoreExternals,
                //TODO: AllowObstructions = Force
            };

            string resolvedPath;
            if (Path == null)
            {
                resolvedPath = GetUnresolvedProviderPathFromPSPath(Url.Segments.Last());
            }
            else
            {
                resolvedPath = GetUnresolvedProviderPathFromPSPath(Path);
            }

            SvnClient.CheckOut(new SvnUriTarget(Url), resolvedPath, args);
        }

        protected override string GetProcessTitle() => "Checking out...";
    }
}
