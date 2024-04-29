// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnUnlock")]
    [Alias("svn-unlock")]
    [OutputType(typeof(SvnNotifyOutput))]
    public class SvnUnlockCmdlet : SvnClientCmdletBase
    {
        [Parameter(Mandatory = true, Position = 0,
                   ValueFromRemainingArguments = true, ValueFromPipeline = true)]
        public SvnTarget[] Target { get; set; }

        [Parameter()]
        [Alias("BreakLock")]
        public SwitchParameter Force { get; set; }

        protected override void Execute()
        {
            SvnUnlockArgs args = new SvnUnlockArgs
            {
                BreakLock = Force,
            };

            ResolvedTargetCollection target = ResolveTargets(Target);
            target.ThrowIfHasAnyOperationalRevisions(nameof(Target));

            if (target.HasPaths)
            {
                SvnClient.Unlock(target.Paths, args);
            }
            else if (target.HasUris)
            {
                SvnClient.RemoteUnlock(target.Urls, args);
            }
            else
            {
                throw new ArgumentException("No targets are specified.", "Target");
            }
        }
    }
}
