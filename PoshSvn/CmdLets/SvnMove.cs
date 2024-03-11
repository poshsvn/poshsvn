// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnMove")]
    [Alias("svn-move")]
    [OutputType(typeof(SvnNotifyOutput), typeof(SvnCommitOutput))]
    public class SvnMove : SvnClientCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string[] Source { get; set; }

        [Parameter(Position = 1, Mandatory = true, ParameterSetName = TargetParameterSetNames.Target)]
        public string Destination { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Path)]
        public string DestinationPath { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Url)]
        public Uri DestinationUrl { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Target)]
        [Parameter(ParameterSetName = TargetParameterSetNames.Url, Mandatory = true)]
        [Alias("m")]
        public string Message { get; set; }

        [Parameter()]
        public SwitchParameter Force { get; set; }

        [Parameter()]
        public SwitchParameter Parents { get; set; }

        [Parameter()]
        public SwitchParameter AllowMixedRevisions { get; set; }

        protected override void Execute()
        {
            SvnMoveArgs args = new SvnMoveArgs
            {
                Force = Force,
                CreateParents = Parents,
                AllowMixedRevisions = AllowMixedRevisions,
                LogMessage = Message,
            };

            TargetCollection sources = TargetCollection.Parse(GetTargets(Source, null, null, true));
            sources.ThrowIfHasPathsAndUris();
            object destination = GetTarget(Destination, DestinationPath, DestinationUrl);

            if (destination is string destinationPath)
            {
                SvnClient.Move(sources.Paths, destinationPath, args);
            }
            else if (destination is Uri destinationUrl)
            {
                SvnClient.RemoteMove(sources.Uris, destinationUrl, args);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
