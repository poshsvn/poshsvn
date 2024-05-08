// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnMkdir", DefaultParameterSetName = ParameterSetNames.Local)]
    [Alias("svn-mkdir")]
    [OutputType(typeof(SvnNotifyOutput))]
    public class SvnMkDir : SvnClientCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public SvnTarget[] Target { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Remote)]
        [Alias("m")]
        public string Message { get; set; }

        [Parameter()]
        public SwitchParameter Parents { get; set; }

        protected override void Execute()
        {
            SvnCreateDirectoryArgs args = new SvnCreateDirectoryArgs
            {
                CreateParents = Parents,
                LogMessage = Message
            };

            ResolvedTargetCollection targets = ResolveTargets(Target);
            targets.ThrowIfHasPathsAndUris(nameof(Target));
            targets.ThrowIfHasAnyOperationalRevisions(nameof(Target));

            if (targets.HasPaths)
            {
                SvnClient.CreateDirectories(targets.Paths, args);
            }
            else
            {
                UpdateProgressAction("Creating transaction...");
                SvnClient.RemoteCreateDirectories(targets.Urls, args);
            }
        }

        protected override string GetProcessTitle() => "svn-mkdir";
    }
}
