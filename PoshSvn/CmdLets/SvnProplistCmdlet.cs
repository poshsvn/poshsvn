// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{

    [Cmdlet("Invoke", "SvnProplist", DefaultParameterSetName = ParameterSetNames.Node)]
    [Alias("svn-proplist")]
    [OutputType(typeof(SvnProperty))]
    public class SvnProplistCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true)]
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

        public SvnProplistCmdlet()
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

                    SvnClient.GetRevisionPropertyList(url, sharpSvnRevision,
                                                      out SvnPropertyCollection properties);

                    foreach (SvnPropertyValue property in properties)
                    {
                        WriteObject(new SvnProperty
                        {
                            Name = property.Key,
                            Value = property.StringValue,
                            Path = url.OriginalString
                        });
                    }
                }
            }
            else
            {
                SvnPropertyListArgs args = new SvnPropertyListArgs
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
                    SvnClient.PropertyList(target, args, PropertyReciever);
                }
            }
        }

        private void PropertyReciever(object sender, SvnPropertyListEventArgs e)
        {
            foreach (SvnPropertyValue property in e.Properties)
            {
                WriteObject(new SvnProperty
                {
                    Name = property.Key,
                    Value = property.StringValue,
                    Path = e.Path,
                });
            }
        }
    }
}
