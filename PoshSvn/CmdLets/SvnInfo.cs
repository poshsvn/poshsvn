﻿// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.Generic;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnInfo")]
    [Alias("svn-info")]
    [OutputType(typeof(SvnInfoOutput))]
    public class SvnInfo : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true)]
        public SvnTarget[] Target { get; set; }

        [Parameter()]
        [Alias("rev")]
        public SharpSvn.SvnRevision Revision { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; }

        [Parameter()]
        [Alias("include-externals")]
        public SwitchParameter IncludeExternals { get; set; }

        public SvnInfo()
        {
            Depth = SvnDepth.Empty;
            Target = new SvnTarget[]
            {
                SvnTarget.FromPath(".")
            };
        }

        protected override void Execute()
        {
            SvnInfoArgs args = new SvnInfoArgs
            {
                Revision = Revision,
                IncludeExternals = IncludeExternals,
                Depth = Depth.ConvertToSharpSvnDepth(),
            };

            ResolvedTargetCollection resolvedTargets = ResolveTargets(Target);

            foreach (SharpSvn.SvnTarget target in resolvedTargets.EnumerateSharpSvnTargets())
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
                NodeKind = e.NodeKind.ToPoshSvnNodeKind(),
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

            if (e.NodeKind == SharpSvn.SvnNodeKind.File)
            {
                svnInfo.Checksum = e.Checksum;
            }

            WriteObject(svnInfo);

            UpdateProgressAction(e.HasLocalInfo ? e.Path : e.Uri.ToString());
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
