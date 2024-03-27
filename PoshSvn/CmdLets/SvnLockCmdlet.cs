// Copyright (c) Timofei Zhakov. All rights reserved.

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
        public string Comment { get; set; }

        protected override void Execute()
        {
            SvnLockArgs args = new SvnLockArgs
            {
                Comment = Comment,
            };

            TargetCollection target = TargetCollection.Parse(GetTargets(Target));

            if (target.HasPaths)
            {
                SvnClient.Lock(target.Paths, args);
            }
        }
    }
}
