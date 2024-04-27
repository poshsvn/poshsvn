// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnSwitch")]
    [Alias("svn-switch")]
    [OutputType(typeof(SvnNotifyOutput))]
    public class SvnSwitch : SvnClientCmdletBase
    {
        [Parameter(Position = 0)]
        public Uri Url { get; set; }

        [PSDefaultValue(Value = ".")]
        [Parameter(Position = 1)]
        public string Path { get; set; } = ".";

        [Parameter()]
        [Alias("rev")]
        public SharpSvn.SvnRevision Revision { get; set; } = null;

        [Parameter()]
        [PSDefaultValue(Value = "Infinity")]
        public SvnDepth Depth { get; set; } = SvnDepth.Infinity;

        [Parameter()]
        [Alias("ignore-externals")]
        public SwitchParameter IgnoreExternals { get; set; }

        protected override string GetActivityTitle(SvnNotifyEventArgs e)
        {
            return "svn-switch";
        }

        protected override void Execute()
        {
            string path = GetUnresolvedProviderPathFromPSPath(Path);
            SvnUriTarget target = SvnUriTarget.FromUri(Url);

            var args = new SvnSwitchArgs
            {
                Revision = Revision,
                Depth = Depth.ConvertToSharpSvnDepth(),
                IgnoreExternals = IgnoreExternals,
            };

            SvnClient.Switch(path, target, args);
        }
    }
}
