// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Linq;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnRevert")]
    [Alias("svn-revert")]
    [OutputType(typeof(SvnNotifyOutput))]
    public class SvnRevert : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromPipeline = true, Mandatory = true, ValueFromRemainingArguments = true)]
        public string[] Path { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Empty;

        [Parameter()]
        public SwitchParameter Recursive { get; set; }

        [Parameter()]
        [Alias("remove-added")]
        public SwitchParameter RemoveAdded { get; set; }

        protected override void Execute()
        {
            var args = new SvnRevertArgs
            {
                AddedKeepLocal = !RemoveAdded,
            };

            if (Recursive)
            {
                args.Depth = SharpSvn.SvnDepth.Infinity;
            }
            else
            {
                args.Depth = Depth.ConvertToSharpSvnDepth();
            }

            SvnClient.Revert(GetPathTargets(Path, true).ToArray(), args);
        }
    }
}
