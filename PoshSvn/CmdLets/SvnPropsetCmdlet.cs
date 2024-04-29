// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnPropset")]
    [Alias("svn-propset")]
    [OutputType(typeof(SvnProperty))]
    public class SvnPropsetCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("propname")]
        public string PropertyName { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        [Alias("propval")]
        public string PropertyValue { get; set; }

        [Parameter(Position = 2, ValueFromRemainingArguments = true, Mandatory = true)]
        public string[] Path { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Empty;

        protected override void Execute()
        {
            SvnSetPropertyArgs args = new SvnSetPropertyArgs
            {
                Depth = Depth.ConvertToSharpSvnDepth()
            };

            foreach (string path in GetPathTargets(Path, true))
            {
                SvnClient.SetProperty(path, PropertyName, PropertyValue, args);
            }
        }
    }
}
