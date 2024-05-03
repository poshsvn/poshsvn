// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.Generic;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnCopy", DefaultParameterSetName = ParameterSetNames.Local)]
    [Alias("svn-copy")]
    [OutputType(typeof(SvnNotifyOutput), typeof(SvnCommitOutput))]
    public class SvnCopy : SvnClientCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true)]
        public SvnTarget[] Source { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public SvnTarget Destination { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetNames.Remote)]
        [Alias("m")]
        public string Message { get; set; }

        [Parameter()]
        [Alias("rev")]
        public SharpSvn.SvnRevision Revision { get; set; }

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
                AlwaysCopyAsChild = true
            };

            ResolvedTargetCollection resolvedSources = ResolveTargets(Source);
            ICollection<SharpSvn.SvnTarget> sources = resolvedSources.ConvertToSharpSvnTargets();

            SvnResolvedTarget resolvedDestination = ResolveTarget(Destination);
            resolvedDestination.ThrowIfHasOperationalRevision(nameof(Destination));

            if (resolvedDestination.TryGetPath(out string destinationPath))
            {
                SvnClient.Copy(sources, destinationPath, args);
            }
            else if (resolvedDestination.TryGetUrl(out Uri destinationUrl))
            {
                SvnClient.RemoteCopy(sources, destinationUrl, args);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
