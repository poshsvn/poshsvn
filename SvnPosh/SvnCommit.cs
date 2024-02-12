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

        private readonly ProgressRecord progressRecord = new ProgressRecord(0, "Commit", "Initializing...");

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
                        WriteVerbose("Committing transaction...");
                        progressRecord.StatusDescription = "Committing transaction...";
                        WriteProgress(progressRecord);
                    }
                    else if (e.Action == SvnNotifyAction.CommitSendData)
                    {
                        progressRecord.StatusDescription = "Transmitting file data...";
                        WriteProgress(progressRecord);
                    }
                    else
                    {
                        progressRecord.StatusDescription = e.Path;
                        WriteProgress(progressRecord);
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
                    progressRecord.CurrentOperation = SvnUtils.FormatProgress(e.Progress);
                    WriteProgress(progressRecord);
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
    }

    public class SvnCommitOutput
    {
        public long Revision { get; set; }
    }
}
