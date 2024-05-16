// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnUpgrade")]
    [Alias("svn-upgrade")]
    [OutputType(typeof(SvnNotifyOutput))]
    public class SvnUpgradeCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true)]
        public string[] Path { get; set; }

        public SvnUpgradeCmdlet()
        {
            Path = new string[]
            {
                "."
            };
        }

        protected override void Execute()
        {
            foreach (string resolvedPath in GetPathTargets(Path, false))
            {
                SvnUpgradeArgs args = new SvnUpgradeArgs
                {
                };

                SvnClient.Upgrade(resolvedPath, args);
            }
        }

        protected override string GetProcessTitle() => "Upgrading working copy...";
    }
}
