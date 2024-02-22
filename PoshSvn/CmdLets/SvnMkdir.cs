using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnMkdir", DefaultParameterSetName = "Path")]
    [Alias("svn-mkdir")]
    [OutputType(typeof(SvnNotifyOutput))]
    public class SvnMkDir : SvnCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Path", ValueFromRemainingArguments = true)]
        public string[] Path { get; set; }

        [Parameter(ParameterSetName = "Url", Mandatory = true)]
        public Uri[] Url { get; set; }

        [Parameter(ParameterSetName = "Url", Mandatory = true)]
        [Alias("m")]
        public string Message { get; set; }

        [Parameter()]
        public SwitchParameter Parents { get; set; }

        protected override string GetActivityTitle(SvnNotifyEventArgs e) => "Creating directory";

        protected override object GetNotifyOutput(SvnNotifyEventArgs e)
        {
            return new SvnNotifyOutput
            {
                Action = e.Action,
                Path = e.Path
            };
        }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                SvnCreateDirectoryArgs args = new SvnCreateDirectoryArgs
                {
                    CreateParents = Parents,
                    LogMessage = Message
                };

                args.Notify += NotifyEventHandler;
                args.Progress += ProgressEventHandler;
                args.Committing += CommittingEventHandler;
                args.Committed += CommittedEventHandler;

                if (Path != null)
                {

                    string[] resolvedPaths = GetPathTargets(null, Path);
                    // TODO: maybe, I'll do it after
                    // int filesProcessedCount = 0;

                    client.CreateDirectories(resolvedPaths, args);
                }
                else
                {
                    UpdateAction("Creating transaction...");
                    client.RemoteCreateDirectories(Url, args);
                }
            }
        }
    }
}
