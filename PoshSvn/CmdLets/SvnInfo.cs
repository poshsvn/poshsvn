using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnInfo", DefaultParameterSetName = TargetParameterSetNames.Target)]
    [Alias("svn-info")]
    [OutputType(typeof(SvnInfoOutput))]
    public class SvnInfo : SvnCmdletBase
    {
        [Parameter(Position = 0, ParameterSetName = TargetParameterSetNames.Target, ValueFromRemainingArguments = true)]
        public string[] Target { get; set; } = new string[] { "" };

        [Parameter(ParameterSetName = TargetParameterSetNames.Path)]
        public string[] Path { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Url)]
        public Uri[] Url { get; set; }

        [Parameter()]
        [Alias("rev")]
        public SvnRevision Revision { get; set; } = null;

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Empty;

        [Parameter()]
        [Alias("include-externals")]
        public SwitchParameter IncludeExternals { get; set; }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    SvnInfoArgs args = new SvnInfoArgs
                    {
                        Revision = Revision,
                        IncludeExternals = IncludeExternals,
                        Depth = Depth.ConvertToSharpSvnDepth(),
                    };

                    args.Progress += Progress;

                    TargetCollection targets = TargetCollection.Create(GetTargets(Target, Path, Url));

                    foreach (SvnTarget target in targets.Targets)
                    {
                        client.Info(target, args, InfoHandler);
                    }
                }
                catch (SvnException ex)
                {
                    if (ex.ContainsError(SvnErrorCode.SVN_ERR_WC_NOT_WORKING_COPY))
                    {
                        WriteWarning(ex.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
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
