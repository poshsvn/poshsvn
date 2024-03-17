// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnInfo", DefaultParameterSetName = ParameterSetNames.Target)]
    [Alias("svn-info")]
    [OutputType(typeof(SvnInfoOutput))]
    public class SvnInfo : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ParameterSetName = ParameterSetNames.Target, ValueFromRemainingArguments = true)]
        public string[] Target { get; set; } = new string[] { "" };

        [Parameter(ParameterSetName = ParameterSetNames.Path)]
        public string[] Path { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Url)]
        public Uri[] Url { get; set; }

        [Parameter()]
        [Alias("rev")]
        public SvnRevision Revision { get; set; } = null;

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Empty;

        [Parameter()]
        [Alias("include-externals")]
        public SwitchParameter IncludeExternals { get; set; }

        protected override void Execute()
        {
            SvnInfoArgs args = new SvnInfoArgs
            {
                Revision = Revision,
                IncludeExternals = IncludeExternals,
                Depth = Depth.ConvertToSharpSvnDepth(),
            };

            TargetCollection targets = TargetCollection.Parse(GetTargets(Target, Path, Url, true));

            foreach (SvnTarget target in targets.Targets)
            {
                SvnClient.Info(target, args, InfoHandler);
            }
        }

        protected override string GetActivityTitle(SvnNotifyEventArgs e)
        {
            return "svn-info";
        }

        private void InfoHandler(object sender, SvnInfoEventArgs e)
        {
            SvnInfoOutput svnInfo = new SvnInfoOutput
            {
                Path = e.Path,
                Url = e.Uri,
                RelativeUrl = e.RepositoryRoot.MakeRelativeUri(e.Uri),
                RepositoryRoot = e.RepositoryRoot,
                RepositoryId = e.RepositoryId,
                Revision = e.Revision,
                NodeKind = e.NodeKind,
                LastChangedAuthor = e.LastChangeAuthor,
                LastChangedRevision = e.LastChangeRevision,
            };

            if (e.LastChangeTime != DateTime.MinValue)
            {
                svnInfo.LastChangedDate = new DateTimeOffset(e.LastChangeTime);
            }

            if (e.ContentTime != DateTime.MinValue)
            {
                svnInfo.TextLastUpdated = new DateTimeOffset(e.ContentTime);
            }

            if (e.HasLocalInfo)
            {
                svnInfo.Schedule = e.Schedule;
                svnInfo.WorkingCopyRoot = e.WorkingCopyRoot;
            }

            if (e.NodeKind == SvnNodeKind.File)
            {
                svnInfo.Checksum = e.Checksum;
            }

            WriteObject(svnInfo);

            UpdateAction(e.HasLocalInfo ? e.Path : e.Uri.ToString());
        }
    }

    public class SvnInfoOutput
    {
        public string Path { get; set; }
        public Uri Url { get; set; }
        public Uri RelativeUrl { get; set; }
        public Uri RepositoryRoot { get; set; }
        public Guid RepositoryId { get; set; }
        public long Revision { get; set; }
        public SvnNodeKind NodeKind { get; set; }
        public string LastChangedAuthor { get; set; }
        public long LastChangedRevision { get; set; }
        public DateTimeOffset LastChangedDate { get; set; }

        public SvnSchedule? Schedule { get; set; } = null;
        public string WorkingCopyRoot { get; set; } = null;

        public DateTimeOffset? TextLastUpdated { get; set; } = null;
        public string Checksum { get; set; } = null;
    }
}
