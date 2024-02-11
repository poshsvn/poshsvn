using SharpSvn;
using System;
using System.Management.Automation;

namespace SvnPosh
{
    [Cmdlet("Invoke", "SvnCommit")]
    [Alias("svn-commit")]
    [OutputType(typeof(SvnCommitOutput))]
    public class SvnCommit : SvnCmdletBase
    {
        [Parameter(Position = 0)]
        public string[] Path { get; set; } = new string[] { "" };

        [Parameter(Mandatory = true)]
        public string Message { get; set; }

        private long progress = 0;

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                SvnCommitArgs args = new SvnCommitArgs
                {
                    LogMessage = Message,
                };
                args.Notify += new EventHandler<SvnNotifyEventArgs>((_, e) =>
                {
                    if (e.Action == SvnNotifyAction.CommitFinalizing)
                    {
                        WriteVerbose(string.Format("Committing transaction..."));
                    }
                    else
                    {
                        WriteProgress(new ProgressRecord(0, "Committing", string.Format("{0,-24} {1}", SvnUtils.GetCommitActionString(e.Action), e.Path)));
                        WriteVerbose(string.Format("{0,-24} {1}", SvnUtils.GetCommitActionString(e.Action), e.Path));
                    }
                });
                args.Committed += new EventHandler<SvnCommittedEventArgs>((_, e) =>
                {
                    WriteObject(new SvnCommitOutput
                    {
                        Revision = e.Revision
                    });
                });
                args.Progress += new EventHandler<SvnProgressEventArgs>((_, e) =>
                {
                    WriteProgress(new ProgressRecord(1, "Transfered", string.Format("{0,10} kb", e.Progress / 1024))
                    {
                        ParentActivityId = 0
                    });
                });

                try
                {
                    client.Commit(GetPathTargets(Path, null), args);
                }
                catch (SvnException ex)
                {
                    if (ex.ContainsError(SvnErrorCode.SVN_ERR_WC_NOT_WORKING_COPY,
                                         SvnErrorCode.SVN_ERR_WC_PATH_NOT_FOUND))
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

    public class SvnCommitOutput
    {
        public long Revision { get; set; }
    }
}
