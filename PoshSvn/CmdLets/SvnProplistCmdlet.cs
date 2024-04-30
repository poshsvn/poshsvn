// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnProplist")]
    [Alias("svn-proplist")]
    [OutputType(typeof(SvnProperty))]
    public class SvnProplistCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true)]
        public SvnTarget[] Target { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; }

        public SvnProplistCmdlet()
        {
            Target = new SvnTarget[]
            {
                SvnTarget.FromPath(".")
            };
        }

        protected override void Execute()
        {
            SvnPropertyListArgs args = new SvnPropertyListArgs
            {
               Depth = Depth.ConvertToSharpSvnDepth()
            };

            ResolvedTargetCollection resolvedTargets = ResolveTargets(Target);

            foreach (SharpSvn.SvnTarget target in resolvedTargets.EnumerateSharpSvnTargets())
            {
                SvnClient.PropertyList(target, args, PropertyReciever);
            }
        }

        private void PropertyReciever(object sender, SvnPropertyListEventArgs e)
        {
            foreach (SvnPropertyValue property in e.Properties)
            {
                WriteObject(new SvnProperty(property));
            }
        }
    }
}
