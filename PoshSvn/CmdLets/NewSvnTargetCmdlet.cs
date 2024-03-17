// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;

namespace PoshSvn.CmdLets
{
    [Cmdlet("New", "SvnTarget", DefaultParameterSetName = ParameterSetNames.PathOrUrl)]
    [OutputType(typeof(SvnTarget))]
    public class NewSvnTargetCmdlet : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = ParameterSetNames.PathOrUrl)]
        public string PathOrUrl { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Path)]
        public string Path { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.LiteralPath)]
        public string LiteralPath { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Url)]
        public string Url { get; set; }

        protected override void ProcessRecord()
        {
            if (ParameterSetName == ParameterSetNames.PathOrUrl)
            {
                WriteObject(new SvnTarget(PathOrUrl));
            }
            else if (ParameterSetName == ParameterSetNames.Path)
            {
                WriteObject(SvnTarget.FromPath(Path));
            }
            else if (ParameterSetName == ParameterSetNames.LiteralPath)
            {
                WriteObject(SvnTarget.FromLiteralPath(LiteralPath));
            }
            else if (ParameterSetName == ParameterSetNames.Url)
            {
                WriteObject(SvnTarget.FromUrl(Url));
            }
        }
    }
}
