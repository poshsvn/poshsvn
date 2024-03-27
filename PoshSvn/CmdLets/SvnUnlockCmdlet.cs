// Copyright (c) Timofei Zhakov. All rights reserved.

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

            TargetCollection target = TargetCollection.Parse(GetTargets(Target));

            if (target.HasPaths)
            {
                SvnClient.Unlock(target.Paths, args);
            }
        }
    }
}
