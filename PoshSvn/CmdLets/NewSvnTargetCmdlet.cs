// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;

namespace PoshSvn.CmdLets
{
    [Cmdlet("New", "SvnTarget", DefaultParameterSetName = TargetParameterSetNames.PathOrUrl)]
    [OutputType(typeof(PoshSvnTarget))]
    public class NewSvnTargetCmdlet : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = TargetParameterSetNames.PathOrUrl)]
        public string PathOrUrl { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Path)]
        public string Path { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.LiteralPath)]
        public string LiteralPath { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Url)]
        public string Url { get; set; }

        protected override void ProcessRecord()
        {
            if (ParameterSetName == TargetParameterSetNames.PathOrUrl)
            {
                WriteObject(new PoshSvnTarget(PathOrUrl));
            }
            else if (ParameterSetName == TargetParameterSetNames.Path)
            {
                WriteObject(PoshSvnTarget.FromPath(Path));
            }
            else if (ParameterSetName == TargetParameterSetNames.LiteralPath)
            {
                WriteObject(PoshSvnTarget.FromLiteralPath(LiteralPath));
            }
            else if (ParameterSetName == TargetParameterSetNames.Url)
            {
                WriteObject(PoshSvnTarget.FromUrl(Url));
            }
        }
    }
}
