using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnCommit")]
    [Alias("svn-commit")]
    [OutputType(typeof(SvnCommitOutput))]
    public class SvnCommit : SvnClientCmdletBase
    {
        [Parameter(Position = 0)]
        public string[] Path { get; set; } = new string[] { "" };

        [Parameter(Mandatory = true)]
        public string Message { get; set; }

        protected override void Execute()
        {
            SvnCommitArgs args = new SvnCommitArgs
            {
                LogMessage = Message,
            };

            args.Notify += NotifyEventHandler;
            args.Progress += ProgressEventHandler;
            args.Committed += CommittedEventHandler;

            try
            {
                SvnClient.Commit(GetPathTargets(Path, null), args);
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

        protected override string GetActivityTitle(SvnNotifyEventArgs e)
        {
            return "Committing";
        }
    }

    public class SvnCommitOutput
    {
        public long Revision { get; set; }
    }
}
