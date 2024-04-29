// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnRelocate")]
    [Alias("svn-relocate")]
    public class SvnRelocateCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true)]
        public Uri From { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public Uri To { get; set; }

        [Parameter(Position = 2, ValueFromRemainingArguments = true)]
        public string[] Path { get; set; }

        [Parameter()]
        public SwitchParameter IgnoreExternals;

        public SvnRelocateCmdlet()
        {
            Path = new string[]
            {
                "."
            };
        }

        protected override void Execute()
        {
            SvnRelocateArgs args = new SvnRelocateArgs
            {
                IgnoreExternals = IgnoreExternals,
            };

            foreach (string path in GetPathTargets(Path, false))
            {
                SvnClient.Relocate(path, From, To, args);
            }
        }
    }
}
