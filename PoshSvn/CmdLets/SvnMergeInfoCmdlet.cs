// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Linq;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnMergeInfo", DefaultParameterSetName = ParameterAttribute.AllParameterSets)]
    [Alias("svn-mergeinfo")]
    [OutputType(typeof(SvnMergeInfoRevision))]
    public class SvnMergeInfoCmdlet : SvnClientCmdletBase
    {
        [Parameter(Mandatory = true, Position = 0)]
        public SvnTarget Source { get; set; }

        [Parameter(Position = 1)]
        public SvnTarget Target { get; set; } = new SvnTarget(".");

        [Parameter(ParameterSetName = ParameterSetNames.ShowRevisions)]
        [Alias("ShowRevs")]
        public ShowRevisions? ShowRevisions { get; set; } = null;

        [Parameter(ParameterSetName = ParameterSetNames.ShowRevisions)]
        public SwitchParameter Log { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.ShowRevisions)]
        public SvnDepth Depth { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.ShowRevisions)]
        public SwitchParameter Recursive { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.ShowRevisions)]
        [Alias("r", "rev")]
        public SvnRevisionRange Revision { get; set; }

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
                    Depth = Depth.ConvertToSharpSvnDepth(Recursive),
                    Range = Revision?.ToSharpSvnRevisionRange(),
                };

                ConfigureRetrieveProperties(args.RetrieveProperties);

                SvnClient.ListMergesEligible(sharpSvnTarget, sharpSvnSource, args, MergesEligibleReceiver);
            }
            else if (ShowRevisions == PoshSvn.ShowRevisions.Merged)
            {
                SvnMergesMergedArgs args = new SvnMergesMergedArgs
                {
                    Depth = Depth.ConvertToSharpSvnDepth(),
                    Range = Revision?.ToSharpSvnRevisionRange(),
                };

                ConfigureRetrieveProperties(args.RetrieveProperties);

                SvnClient.ListMergesMerged(sharpSvnTarget, sharpSvnSource, args, MergesMergedReceiver);
            }
            else
            {
                SvnMergingSummaryArgs args = new SvnMergingSummaryArgs
                {
                };

                SvnClient.GetMergingSummary(sharpSvnTarget, sharpSvnSource,
                                            args, out SvnMergingSummaryEventArgs mergingSummary);

                WriteObject(new SvnMergeInfo
                {
                    IsReintegration = mergingSummary.IsReintegration,
                    RepositoryRootUrl = mergingSummary.RepositoryRootUrl,
                    YoungestCommonAncestorUrl = mergingSummary.YoungestCommonAncestorUrl,
                    YoungestCommonAncestorRevision = mergingSummary.YoungestCommonAncestorRevision,
                    BaseUrl = mergingSummary.BaseUrl,
                    BaseRevision = mergingSummary.BaseRevision,
                    RightUrl = mergingSummary.RightUrl,
                    RightRevision = mergingSummary.RightRevision,
                    TargetUrl = mergingSummary.TargetUrl,
                    TargetRevision = mergingSummary.TargetRevision,
                });
            }
        }

        private void MergesEligibleReceiver(object sender, SvnMergesEligibleEventArgs e)
        {
            if (Log)
            {
                WriteObject(CreateLogOutput(e));
            }
            else
            {
                SvnMergeInfoRevision obj = CreateMergeInfoRevision(e);
                obj.SourceUri = e.SourceUri;
                WriteObject(obj);
            }
        }

        private void MergesMergedReceiver(object sender, SvnMergesMergedEventArgs e)
        {
            if (Log)
            {
                WriteObject(CreateLogOutput(e));
            }
            else
            {
                SvnMergeInfoRevision obj = CreateMergeInfoRevision(e);
                obj.SourceUri = e.SourceUri;
                WriteObject(obj);
            }
        }

        private void ConfigureRetrieveProperties(SvnRevisionPropertyNameCollection retrieveProperties)
        {
            retrieveProperties.Clear();

            if (Log)
            {
                retrieveProperties.Add(SvnRevisionPropertyNameCollection.Log);
                retrieveProperties.Add(SvnRevisionPropertyNameCollection.Author);
                retrieveProperties.Add(SvnRevisionPropertyNameCollection.Date);
            }
        }

        private SvnMergeInfoRevision CreateMergeInfoRevision(SvnLoggingEventArgs e)
        {
            return new SvnMergeInfoRevision
            {
                Revision = e.Revision,
                LogMessage = e.LogMessage,
                Date = e.Time,
                Author = e.Author,
            };
        }

        private SvnLogOutput CreateLogOutput(SvnLoggingEventArgs e)
        {
            SvnLogOutput obj = new SvnLogOutput
            {
                Revision = e.Revision,
                Author = e.Author,
                Message = e.LogMessage,
                RevisionProperties = e.RevisionProperties.ToPoshSvnPropertyCollection(),
            };

            if (e.Time != DateTime.MinValue)
            {
                obj.Date = new DateTimeOffset(e.Time);
            }

            return obj;
        }
    }
}
