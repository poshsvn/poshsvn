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
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = TargetParameterSetNames.Target,
                   ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public string[] Target { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Path, Mandatory = true)]
        public string[] Path { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Url, Mandatory = true)]
        public Uri[] Url { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Target)]
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

                args.Progress += ProgressEventHandler;
                args.Notify += NotifyEventHandler;
                args.Committed += CommittedEventHandler;

                TargetCollection targets = TargetCollection.Parse(GetTargets(Target, Path, Url, true));
                targets.ThrowIfHasPathsAndUris();

                if (targets.HasPaths)
                {
                    client.Delete(targets.Paths, args);
                }
                else
                {
                    client.RemoteDelete(targets.Uris, args);
                }
            }
        }
    }
}
