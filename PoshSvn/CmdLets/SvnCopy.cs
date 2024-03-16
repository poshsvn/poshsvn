// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnCopy", DefaultParameterSetName = TargetParameterSetNames.Target)]
    [Alias("svn-copy")]
    [OutputType(typeof(SvnNotifyOutput), typeof(SvnCommitOutput))]
    public class SvnCopy : SvnClientCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = TargetParameterSetNames.Target)]
        public string[] Source { get; set; }

        [Parameter(Position = 1, Mandatory = true, ParameterSetName = TargetParameterSetNames.Target)]
        public string Destination { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Path)]
        public string DestinationPath { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Url)]
        public string DestinationUrl { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Target)]
        [Parameter(ParameterSetName = TargetParameterSetNames.Url, Mandatory = true)]
        [Alias("m")]
        public string Message { get; set; }

        [Parameter()]
        [Alias("rev")]
        public SvnRevision Revision { get; set; }

        [Parameter()]
        [Alias("ignore-externals")]
        public SwitchParameter IgnoreExternals { get; set; }

        [Parameter()]
        public SwitchParameter Parents { get; set; }

        protected override void Execute()
        {
            SvnCopyArgs args = new SvnCopyArgs
            {
                CreateParents = Parents,
                LogMessage = Message,
                Revision = Revision,
                IgnoreExternals = IgnoreExternals,
            };

            TargetCollection sources = TargetCollection.Parse(GetTargets(Source, null, null, true));
            object destination = GetTarget(Destination, null, null);

            if (destination is string destinationPath)
            {
                SvnClient.Copy(sources.Targets, destinationPath, args);
            }
            else if (destination is Uri destinationUrl)
            {
                SvnClient.RemoteCopy(sources.Targets, destinationUrl, args);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
