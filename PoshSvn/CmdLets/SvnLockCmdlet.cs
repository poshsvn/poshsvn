// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnLock")]
    [Alias("svn-lock")]
    [OutputType(typeof(SvnNotifyOutput))]
    public class SvnLockCmdlet : SvnClientCmdletBase
    {
        [Parameter(Mandatory = true, Position = 0,
                   ValueFromRemainingArguments = true, ValueFromPipeline = true)]
        public SvnTarget[] Target { get; set; }

        [Parameter()]
        [Alias("Comment")]
        public string Message { get; set; }

        [Parameter()]
        [Alias("StealLock")]
        public SwitchParameter Force { get; set; }

        protected override void Execute()
        {
            SvnLockArgs args = new SvnLockArgs
            {
                Comment = Message,
                StealLock = Force,
            };

            TargetCollection target = TargetCollection.Parse(GetTargets(Target));

            if (target.HasPaths)
            {
                SvnClient.Lock(target.Paths, args);
            }
            else if (target.HasUris)
            {
                SvnClient.RemoteLock(target.Uris, args);
            }
            else
            {
                throw new ArgumentException("No targets are specified.", "Target");
            }
        }
    }
}
