using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnCommit")]
    [Alias("svn-commit")]
    [OutputType(typeof(SvnCommitOutput))]
    public class SvnCommit : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true)]
        public string[] Path { get; set; } = new string[] { "" };

        [Parameter(Mandatory = true)]
        public string Message { get; set; }

        protected override void Execute()
        {
            SvnCommitArgs args = new SvnCommitArgs
            {
                LogMessage = Message,
            };

            SvnClient.Commit(GetPathTargets(Path, null), args);
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
