using System.Management.Automation;
using SharpSvn;

namespace PoshSvn
{
    [Cmdlet("Invoke", "SvnDelete")]
    [Alias("svn-delete", "svn-remove", "Remove-SvnItem")]
    [OutputType(typeof(SvnCommitOutput))]
    public class SvnDelete : SvnCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public string[] Path { get; set; }

        [Parameter()]
        [Alias("f")]
        public SwitchParameter Force { get; set; }

        [Parameter()]
        [Alias("keep-local")]
        public SwitchParameter KeepLocal { get; set; }

        // TODO: with log message (remote)

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                SvnDeleteArgs args = new SvnDeleteArgs
                {
                    Force = Force,
                    KeepLocal = KeepLocal,
                };

                args.Progress += Progress;
                args.Notify += Notify;

                string[] targets = GetPathTargets(Path, null);

                client.Delete(targets, args);
            }
        }

        protected override object GetNotifyOutput(SvnNotifyEventArgs e)
        {
            return new SvnDeleteOutput
            {
                Action = e.Action,
                Path = e.Path,
            };
        }
    }

    public class SvnDeleteOutput
    {
        public SvnNotifyAction Action { get; set; }
        public string ActionString => SvnUtils.GetActionStringShort(Action);
        public string Path { get; set; }
    }
}
