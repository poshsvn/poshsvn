// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;

namespace PoshSvn.CmdLets
{
    [Cmdlet("New", "SvnTarget", DefaultParameterSetName = ParameterSetNames.PathOrUrl)]
    [OutputType(typeof(PoshSvnTarget))]
    public class NewSvnTargetCmdlet : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = ParameterSetNames.PathOrUrl)]
        public string PathOrUrl { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Path)]
        public string Path { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Url)]
        public string Url { get; set; }

        protected override void ProcessRecord()
        {
            if (ParameterSetName == ParameterSetNames.PathOrUrl)
            {
                WriteObject(new PoshSvnTarget(PathOrUrl));
            }
            else if (ParameterSetName == ParameterSetNames.Path)
            {
                WriteObject(PoshSvnTarget.FromPath(Path));
            }
            else if (ParameterSetName == ParameterSetNames.Url)
            {
                WriteObject(PoshSvnTarget.FromUrl(Url));
            }
        }

        private static class ParameterSetNames
        {
            public const string PathOrUrl = "PathOrUrl";
            public const string Path = "Path";
            public const string Url = "Url";
        }
    }
}
