// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using System.Management.Automation;

namespace PoshSvn.CmdLets
{
    [Cmdlet("New", "SvnTarget", DefaultParameterSetName = ParameterSetNames.InputObject)]
    [OutputType(typeof(SvnTarget))]
    public class NewSvnTargetCmdlet : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = ParameterSetNames.InputObject, ValueFromPipeline = true)]
        public PSObject InputObject { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Path)]
        public string Path { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.LiteralPath)]
        public string LiteralPath { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Url)]
        public string Url { get; set; }

        protected override void ProcessRecord()
        {
            if (ParameterSetName == ParameterSetNames.InputObject)
            {
                object baseObject = InputObject.BaseObject;

                if (baseObject is FileSystemInfo fileSystemInfo)
                {
                    WriteObject(new SvnTarget(fileSystemInfo));
                }
                else
                {
                    WriteObject(new SvnTarget(InputObject.ToString()));
                }
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
