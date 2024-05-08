// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnPropget", DefaultParameterSetName = ParameterSetNames.Node)]
    [Alias("svn-propget")]
    [OutputType(typeof(SvnProperty))]
    public class SvnPropgetCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true)]
        [Alias("propname")]
        [ArgumentCompleter(typeof(SvnPropertyNameArgumentCompleter))]
        public string PropertyName { get; set; }

        [Parameter(Position = 1, ValueFromRemainingArguments = true)]
        public SvnTarget[] Target { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; }

        [Parameter()]
        public SwitchParameter Recursive { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Revision)]
        [Alias("revprop")]
        public SwitchParameter RevisionProperty { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Node)]
        [Parameter(ParameterSetName = ParameterSetNames.Revision, Mandatory = true)]
        [Alias("r", "rev")]
        public SvnRevision Revision { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Node)]
        [Alias("cl")]
        public string[] ChangeList { get; set; }

        public SvnPropgetCmdlet()
        {
            Target = new SvnTarget[]
            {
                SvnTarget.FromPath(".")
            };
        }

        protected override void Execute()
        {
            ResolvedTargetCollection resolvedTargets = ResolveTargets(Target);

            if (RevisionProperty)
            {
                foreach (SvnResolvedTarget target in resolvedTargets.Targets)
                {
                    SharpSvn.SvnRevision sharpSvnRevision = Revision.ToSharpSvnRevision();
                    Uri url = GetUrlFromTarget(target);

                    SvnClient.GetRevisionProperty(url, sharpSvnRevision, PropertyName,
                                                  out SvnPropertyValue property);

                    WriteObject(new SvnProperty
                    {
                        Name = property.Key,
                        Value = property.StringValue,
                        Path = url.OriginalString
                    });
                }
            }
            else
            {
                SvnGetPropertyArgs args = new SvnGetPropertyArgs
                {
                    Depth = Depth.ConvertToSharpSvnDepth(Recursive),
                    Revision = Revision?.ToSharpSvnRevision(),
                };

                if (ChangeList != null)
                {
                    foreach (string changelist in ChangeList)
                    {
                        args.ChangeLists.Add(changelist);
                    }
                }

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

        protected override string GetProcessTitle() => "svn-propget";
    }
}
