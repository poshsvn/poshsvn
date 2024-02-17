using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn
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

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                SvnCommitArgs args = new SvnCommitArgs
                {
                    LogMessage = Message,
                };
                args.Notify += Notify;
                args.Progress += Progress;
                args.Committed += new EventHandler<SvnCommittedEventArgs>((_, e) =>
                {
                    WriteObject(new SvnCommitOutput
                    {
                        Revision = e.Revision
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

        protected override string GetActivityTitle(SvnNotifyEventArgs e)
        {
            return "Committing";
        }

        protected override object GetNotifyOutput(SvnNotifyEventArgs e)
        {
            return null;
        }
    }

    public class SvnCommitOutput
    {
        public long Revision { get; set; }
    }
}
