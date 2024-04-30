// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnPropget")]
    [Alias("svn-propget")]
    [OutputType(typeof(SvnProperty))]
    public class SvnPropgetCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("propname")]
        [ArgumentCompleter(typeof(SvnPropertyArgumentCompleter))]
        public string PropertyName { get; set; }

        [Parameter(Position = 1, ValueFromRemainingArguments = true)]
        public SvnTarget[] Target { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; }

        public SvnPropgetCmdlet()
        {
            Target = new SvnTarget[]
            {
                SvnTarget.FromPath(".")
            };
        }

        protected override void Execute()
        {
            SvnGetPropertyArgs args = new SvnGetPropertyArgs
            {
                Depth = Depth.ConvertToSharpSvnDepth(),
            };

            ResolvedTargetCollection resolvedTargets = ResolveTargets(Target);

            foreach (SharpSvn.SvnTarget target in resolvedTargets.EnumerateSharpSvnTargets())
            {
                SvnClient.GetProperty(target, PropertyName, args, out SvnTargetPropertyCollection properties);

                foreach (SvnPropertyValue property in properties)
                {
                    WriteObject(new SvnProperty
                    {
                        Name = property.Key,
                        Value = property.StringValue,
                        Path = property.Target.TargetName,
                    });
                }
            }
        }
    }
}
