// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnList")]
    [Alias("svn-list", "svn-ls")]
    [OutputType(typeof(SvnItem), typeof(SvnItemDetailed))]
    public class SvnList : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true)]
        public SvnTarget[] Target { get; set; }

        [Parameter()]
        [Alias("v")]
        public SwitchParameter Detailed { get; set; }

        [Parameter()]
        [Alias("rev")]
        public SharpSvn.SvnRevision Revision { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; }

        [Parameter()]
        public SwitchParameter Recursive { get; set; }

        [Parameter()]
        [Alias("include-externals")]
        public SwitchParameter IncludeExternals { get; set; }

        public SvnList()
        {
            Depth = SvnDepth.Immediates;
            Target = new SvnTarget[]
            {
                SvnTarget.FromPath(".")
            };
        }

        protected override void Execute()
        {
            SvnListArgs args = new SvnListArgs
            {
                Revision = Revision,
                IncludeExternals = IncludeExternals,
                Depth = Depth.ConvertToSharpSvnDepth(Recursive),
            };

            if (Detailed)
            {
                args.RetrieveEntries = SvnDirEntryItems.AllFieldsV15;
            }

            ResolvedTargetCollection resolvedTargets = ResolveTargets(Target);

            foreach (SharpSvn.SvnTarget target in resolvedTargets.EnumerateSharpSvnTargets())
            {
                SvnClient.List(target, args, ListHandler);
            }
        }

        private void ListHandler(object sender, SvnListEventArgs e)
        {
            if (Detailed || e.Path != "")
            {
                SvnItem obj;

                if (Detailed)
                {
                    obj = new SvnItemDetailed()
                    {
                        Revision = e.Entry.Revision,
                        Author = e.Entry.Author,
                        FileSize = e.Entry.FileSize == -1 ? null : (long?)e.Entry.FileSize,

                        HasProperties = e.Entry.HasProperties,
                    };
                }
                else
                {
                    obj = new SvnItem();
                }

                obj.Date = e.Entry.Time;
                obj.NodeKind = e.Entry.NodeKind.ToPoshSvnNodeKind();
                obj.NodeKind = e.Entry.NodeKind.ToPoshSvnNodeKind();
                obj.Path = e.Path;
                obj.Uri = e.Uri;
                obj.ExternalTarget = e.ExternalTarget;
                obj.ExternalParent = e.ExternalParent;
                obj.BasePath = e.BasePath;
                obj.RepositoryRoot = e.RepositoryRoot;
                obj.Name = e.Name;
                obj.BaseUri = e.BaseUri;

                WriteObject(obj);
            }
        }

        protected override string GetProcessTitle() => "svn-list";
    }
}
