// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
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
        [ArgumentCompleter(typeof(SvnPropertyNameArgumentCompleter))]
        public string PropertyName { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        [Alias("propval")]
        public string PropertyValue { get; set; }

        [Parameter(Position = 2, ValueFromRemainingArguments = true)]
        public SvnTarget[] Target { get; set; }

        [Parameter()]
        [Alias("cl")]
        public string[] ChangeList { get; set; }

        public SvnPropsetCmdlet()
        {
            Target = new SvnTarget[] { SvnTarget.FromPath(".") };
        }

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Empty;

        protected override void Execute()
        {
            ResolvedTargetCollection resolvedTargets = ResolveTargets(Target);

            SvnSetPropertyArgs args = new SvnSetPropertyArgs
            {
                Depth = Depth.ConvertToSharpSvnDepth()
            };

            if (ChangeList != null)
            {
                foreach (string changelist in ChangeList)
                {
                    args.ChangeLists.Add(changelist);
                }
            }

            foreach (SvnResolvedTarget target in resolvedTargets.Targets)
            {
                if (target.TryGetPath(out string path))
                {
                    SvnClient.SetProperty(path, PropertyName, PropertyValue, args);
                }
                else if (target.TryGetUrl(out Uri url))
                {
                    throw new ArgumentException("This cmdlet does not support remote target.", "Target");
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        protected override void HandlePropertyNotifyAction(SvnNotifyEventArgs e)
        {
            WriteObject(new SvnProperty
            {
                Name = e.PropertyName,
                Value = PropertyValue,
                Path = e.Path,
            });
        }
    }
}
