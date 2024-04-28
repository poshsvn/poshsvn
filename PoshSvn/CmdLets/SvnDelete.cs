// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnDelete", DefaultParameterSetName = ParameterSetNames.Local)]
    [Alias("svn-delete", "svn-remove")]
    [OutputType(typeof(SvnCommitOutput))]
    public class SvnDelete : SvnClientCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public SvnTarget[] Target { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Remote)]
        [Alias("m")]
        public string Message { get; set; }

        [Parameter()]
        [Alias("f")]
        public SwitchParameter Force { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Remote)]
        [Alias("keep-local")]
        public SwitchParameter KeepLocal { get; set; }

        protected override void Execute()
        {
            SvnDeleteArgs args = new SvnDeleteArgs
            {
                Force = Force,
                KeepLocal = KeepLocal,
                LogMessage = Message,
            };

            ResolvedTargetCollection targets = ResolveTargets(Target);
            targets.ThrowIfHasPathsAndUris(nameof(Target));
            targets.ThrowIfHasAnyOperationalRevisions(nameof(Target));

            if (targets.HasPaths)
            {
                SvnClient.Delete(targets.Paths, args);
            }
            else
            {
                SvnClient.RemoteDelete(targets.Urls, args);
            }
        }
    }
}
