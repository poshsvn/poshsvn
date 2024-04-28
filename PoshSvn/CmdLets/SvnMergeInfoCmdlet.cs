// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnMergeInfo")]
    [Alias("svn-mergeinfo")]
    [OutputType(typeof(SvnMergeInfo))]
    public class SvnMergeInfoCmdlet : SvnClientCmdletBase
    {
        [Parameter(Mandatory = true, Position = 0)]
        public SvnTarget Target { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public SvnTarget Source { get; set; }

        [Parameter()]
        [Alias("ShowRevs")]
        public ShowRevisions? ShowRevisions { get; set; } = null;

        protected override void Execute()
        {
            SvnResolvedTarget resolvedTarget = ResolveTarget(Target);
            SvnResolvedTarget resolvedSource = ResolveTarget(Source);

            SharpSvn.SvnTarget sharpSvnTarget = resolvedTarget.ConvertToSharpSvnTarget();
            SharpSvn.SvnTarget sharpSvnSource = resolvedSource.ConvertToSharpSvnTarget();

            if (ShowRevisions == PoshSvn.ShowRevisions.Eligible)
            {
                SvnMergesEligibleArgs args = new SvnMergesEligibleArgs
                {
                };

                SvnClient.ListMergesEligible(sharpSvnTarget, sharpSvnSource, args, MergesEligibleReceiver);
            }
            else if (ShowRevisions == PoshSvn.ShowRevisions.Merged)
            {
                SvnMergesMergedArgs args = new SvnMergesMergedArgs
                {
                };

                SvnClient.ListMergesMerged(sharpSvnTarget, sharpSvnSource, args, MergesMergedReceiver);
            }
            else
            {
                // TODO:
                throw new NotImplementedException();
            }
        }

        private void MergesEligibleReceiver(object sender, SvnMergesEligibleEventArgs e)
        {
            WriteObject(new SvnMergeInfo
            {
                Revision = e.Revision,
            });
        }

        private void MergesMergedReceiver(object sender, SvnMergesMergedEventArgs e)
        {
            WriteObject(new SvnMergeInfo
            {
                Revision = e.Revision,
            });
        }
    }
}
