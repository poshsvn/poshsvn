using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnDelete")]
    [Alias("svn-delete", "svn-remove", "Remove-SvnItem")]
    [OutputType(typeof(SvnCommitOutput))]
    public class SvnDelete : SvnCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = TargetParameterSetNames.Path,
                   ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public string[] Path { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Url, Mandatory = true)]
        public Uri[] Url { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Url, Mandatory = true)]
        [Alias("m")]
        public string Message { get; set; }

        [Parameter()]
        [Alias("f")]
        public SwitchParameter Force { get; set; }

        [Parameter()]
        [Alias("keep-local")]
        public SwitchParameter KeepLocal { get; set; }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                SvnDeleteArgs args = new SvnDeleteArgs
                {
                    Force = Force,
                    KeepLocal = KeepLocal,
                    LogMessage = Message,
                };

                args.Progress += Progress;
                args.Notify += Notify;

                if (ParameterSetName == TargetParameterSetNames.Path)
                {
                    client.Delete(GetPathTargets(Path, null), args);
                }
                else
                {
                    client.RemoteDelete(Url, args, out SvnCommitResult result);

                    WriteObject(new SvnCommitOutput
                    {
                        Revision = result.Revision,
                    });
                }
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
