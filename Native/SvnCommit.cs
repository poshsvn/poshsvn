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

        private string state = "Creating transaction...";
        private string action;
        private string path;

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
                        WriteVerbose(state);
                    }
                    else
                    {
                        action = SvnUtils.GetCommitActionString(e.Action);
                        path = e.Path;
                        WriteVerbose(string.Format("{0,-10} {1}", action, path));
                        UpdateProgress();
                    }
                });
                WriteVerbose(state);
                args.Committing += new EventHandler<SvnCommittingEventArgs>((_, e) =>
                {
                    state = "Commiting transaction...";
                    WriteVerbose(state);
                    UpdateProgress();
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
                    WriteProgress(new ProgressRecord(1, "Transfering data...", SvnUtils.FormatBasicProgress(e.Progress))
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

        private void UpdateProgress()
        {
            WriteProgress(new ProgressRecord(0, state, string.Format("{0,-10} {1}", action, path)));
        }
    }

    public class SvnCommitOutput
    {
        public long Revision { get; set; }
    }
}
