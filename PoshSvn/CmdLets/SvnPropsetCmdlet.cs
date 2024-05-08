// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnPropset", DefaultParameterSetName = ParameterSetNames.Node)]
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

        [Parameter(ParameterSetName = ParameterSetNames.Revision)]
        [Alias("revprop")]
        public SwitchParameter RevisionProperty { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Revision, Mandatory = true)]
        [Alias("r", "rev")]
        public SvnRevision Revision { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Node)]
        [Alias("cl")]
        public string[] ChangeList { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; }

        [Parameter()]
        public SwitchParameter Recursive { get; set; }

        public SvnPropsetCmdlet()
        {
            Depth = SvnDepth.Empty;
            Target = new SvnTarget[] { SvnTarget.FromPath(".") };
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

                    SvnClient.SetRevisionProperty(url, sharpSvnRevision, PropertyName, PropertyValue);
                }
            }
            else
            {
                SvnSetPropertyArgs args = new SvnSetPropertyArgs
                {
                    Depth = Depth.ConvertToSharpSvnDepth(Recursive)
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
        }

        protected override void HandlePropertyNotifyAction(SvnNotifyEventArgs e)
        {
            WriteObject(new SvnProperty
            {
                Name = e.PropertyName,
                Value = PropertyValue,
                Path = e.Path ?? e.Uri.OriginalString,
            });
        }

        protected override string GetProcessTitle() => "svn-propset";
    }
}
