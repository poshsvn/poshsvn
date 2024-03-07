using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnMkdir", DefaultParameterSetName = "Path")]
    [Alias("svn-mkdir")]
    [OutputType(typeof(SvnNotifyOutput))]
    public class SvnMkDir : SvnClientCmdletBase
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
        public SwitchParameter Parents { get; set; }

        protected override string GetActivityTitle(SvnNotifyEventArgs e) => "Creating directory";

        protected override void Execute()
        {
            SvnCreateDirectoryArgs args = new SvnCreateDirectoryArgs
            {
                CreateParents = Parents,
                LogMessage = Message
            };

            TargetCollection targets = TargetCollection.Parse(GetTargets(Target, Path, Url, false));
            targets.ThrowIfHasPathsAndUris();

            if (targets.HasPaths)
            {
                SvnClient.CreateDirectories(targets.Paths, args);
            }
            else
            {
                UpdateAction("Creating transaction...");
                SvnClient.RemoteCreateDirectories(targets.Uris, args);
            }
        }
    }
}
