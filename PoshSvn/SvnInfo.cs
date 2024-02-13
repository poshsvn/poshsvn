using SharpSvn;
using System;
using System.Management.Automation;

namespace PoshSvn
{
    [Cmdlet("Invoke", "SvnInfo")]
    [Alias("svn-info")]
    [OutputType(typeof(SvnInfoLocalOutput), typeof(SvnInfoRemoteOutput))]
    public class SvnInfo : SvnCmdletBase
    {
        [Parameter(Position = 0)]
        public string[] Path { get; set; } = new string[] { "" };

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
                foreach (string path in GetPathTargets(Path, null))
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

                        client.Info(SvnTarget.FromString(path), args, InfoHandler);
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
        }

        private void InfoHandler(object sender, SvnInfoEventArgs e)
        {
            WriteObject(new SvnInfoLocalOutput
            {
                Path = e.Path,
                WorkingCopyRoot = e.WorkingCopyRoot,
                Url = e.Uri,
                RelativeUrl = e.RepositoryRoot.MakeRelativeUri(e.Uri),
                RepositoryRoot = e.RepositoryRoot,
                RepositoryId = e.RepositoryId,
                Revision = e.Revision,
                NodeKind = e.NodeKind,
                Schedule = e.Schedule,
                LastChangedAuthor = e.LastChangeAuthor,
                LastChangedRevision = e.LastChangeRevision,
                LastChangedDate = e.LastChangeTime
            });
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
