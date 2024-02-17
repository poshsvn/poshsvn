using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn
{
    [Cmdlet("Invoke", "SvnInfo", DefaultParameterSetName = TargetParameterSetNames.Target)]
    [Alias("svn-info")]
    [OutputType(typeof(SvnInfoLocalOutput), typeof(SvnInfoRemoteOutput))]
    public class SvnInfo : SvnCmdletBase
    {
        [Parameter(Position = 0, ParameterSetName = TargetParameterSetNames.Target, ValueFromRemainingArguments = true)]
        public string[] Target { get; set; } = new string[] { "" };

        [Parameter(ParameterSetName = TargetParameterSetNames.Path)]
        public string[] Path { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Url)]
        public Uri[] Url { get; set; }

        [Parameter()]
        [Alias("r", "rev")]
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

                    foreach (SvnTarget target in GetTargets(Target, Path, Url))
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
            if (e.HasLocalInfo)
            {
                UpdateAction(e.Path);
                SvnInfoLocalOutput svnInfo = new SvnInfoLocalOutput();
                FillSvnInfoOutputProperties(svnInfo, e);
                svnInfo.Schedule = e.Schedule;
                svnInfo.WorkingCopyRoot = e.WorkingCopyRoot;
                WriteObject(svnInfo);
            }
            else
            {
                UpdateAction(e.Uri.ToString());
                SvnInfoRemoteOutput svnInfo = new SvnInfoRemoteOutput();
                FillSvnInfoOutputProperties(svnInfo, e);
                WriteObject(svnInfo);
            }
        }

        private void FillSvnInfoOutputProperties(SvnInfoOutput svnInfo, SvnInfoEventArgs e)
        {
            svnInfo.Path = e.Path;
            svnInfo.Url = e.Uri;
            svnInfo.RelativeUrl = e.RepositoryRoot.MakeRelativeUri(e.Uri);
            svnInfo.RepositoryRoot = e.RepositoryRoot;
            svnInfo.RepositoryId = e.RepositoryId;
            svnInfo.Revision = e.Revision;
            svnInfo.NodeKind = e.NodeKind;
            svnInfo.LastChangedAuthor = e.LastChangeAuthor;
            svnInfo.LastChangedRevision = e.LastChangeRevision;
            svnInfo.LastChangedDate = e.LastChangeTime;
        }
    }

    public abstract class SvnInfoOutput
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
        public DateTime LastChangedDate { get; set; }
    }

    public class SvnInfoLocalOutput : SvnInfoOutput
    {
        public SvnSchedule Schedule { get; set; }
        public string WorkingCopyRoot { get; set; }
    }

    public class SvnInfoRemoteOutput : SvnInfoOutput
    {
    }
}
