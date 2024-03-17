// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Linq;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnCheckout")]
    [Alias("svn-checkout")]
    [OutputType(typeof(SvnCheckoutOutput))]
    public class SvnCheckout : SvnClientCmdletBase
    {
        [Parameter(Mandatory = true, Position = 0)]
        public Uri Url { get; set; }

        [Parameter(Position = 1)]
        public string Path { get; set; }

        [Parameter()]
        [Alias("rev")]
        public SvnRevision Revision { get; set; }

        [Parameter()]
        [Alias("ignore-externals")]
        public SwitchParameter IgnoreExternals { get; set; }

        [Parameter()]
        [Alias("f")]
        public SwitchParameter Force { get; set; }

        protected override string GetActivityTitle(SvnNotifyEventArgs e)
        {
            return e == null ? "Checking out" : string.Format("Checking out '{0}'", e.Path);
        }

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
    }

    public class SvnCheckoutOutput
    {
        public long Revision { get; set; }
    }
}
