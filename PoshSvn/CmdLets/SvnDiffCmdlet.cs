// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using System.Management.Automation;
using System.Text;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnDiff", DefaultParameterSetName = ParameterSetNames.Target)]
    [Alias("svn-diff", "svn-di")]
    [OutputType(typeof(string))]
    public class SvnDiffCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true, ParameterSetName = ParameterSetNames.Target)]
        public SvnTarget[] Target { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.TwoFiles)]
        public SvnTarget Old { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.TwoFiles)]
        public SvnTarget New { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; }

        [Parameter()]
        public SwitchParameter Recursive { get; set; }

        [Parameter()]
        public SwitchParameter NoDiffAdded { get; set; }

        [Parameter()]
        public SwitchParameter NoDiffDeleted { get; set; }

        [Parameter()]
        public SwitchParameter IgnoreProperties { get; set; }

        [Parameter()]
        public SwitchParameter PropertiesOnly { get; set; }

        [Parameter()]
        public SwitchParameter ShowCopiesAsAdds { get; set; }

        [Parameter()]
        public SwitchParameter NoticeAncestry { get; set; }

        [Parameter()]
        [Alias("cl")]
        public string Changelist { get; set; }

        [Parameter()]
        public SwitchParameter Git { get; set; }

        [Parameter()]
        public SwitchParameter PatchCompatible { get; set; }

        public SvnDiffCmdlet()
        {
            Depth = SvnDepth.Infinity;
            Target = new[]
            {
                SvnTarget.FromPath(".")
            };
        }

        protected override void Execute()
        {
            SharpSvn.SvnDiffArgs args = new SharpSvn.SvnDiffArgs
            {
                Depth = Depth.ConvertToSharpSvnDepth(Recursive),
                NoAdded = NoDiffAdded,
                NoDeleted = NoDiffDeleted,
                NoProperties = IgnoreProperties || PatchCompatible,
                PropertiesOnly = PropertiesOnly,
                CopiesAsAdds = ShowCopiesAsAdds || PatchCompatible,
                IgnoreAncestry = !NoticeAncestry,
                UseGitFormat = Git,
            };

            if (Changelist != null)
            {
                args.ChangeLists.Add(Changelist);
            }

            if (ParameterSetName == ParameterSetNames.Target)
            {
                SharpSvn.SvnRevision startRevision = new SharpSvn.SvnRevision(SharpSvn.SvnRevisionType.Base);
                SharpSvn.SvnRevision endRevision = new SharpSvn.SvnRevision(SharpSvn.SvnRevisionType.Working);
                SharpSvn.SvnRevisionRange rangeRevision = new SharpSvn.SvnRevisionRange(startRevision, endRevision);

                ResolvedTargetCollection resolvedTarget = ResolveTargets(Target);

                foreach (SharpSvn.SvnTarget target in resolvedTarget.EnumerateSharpSvnTargets())
                {
                    using (Stream stream = GetStream())
                    {
                        SvnClient.Diff(target, rangeRevision, args, stream);
                    }
                }
            }
            else
            {
                using (Stream stream = GetStream())
                {
                    SharpSvn.SvnTarget oldTarget = ResolveTarget(Old).ConvertToSharpSvnTarget();
                    SharpSvn.SvnTarget newTarget = ResolveTarget(New).ConvertToSharpSvnTarget();

                    SvnClient.Diff(oldTarget, newTarget, args, stream);
                };
            }
        }

        protected Stream GetStream()
        {
            ITextLineStream textStream = new TextLineStream(this);
            ITextStream lineStream = new LineDecoderTextStream(textStream);

            return new DecoderStream(lineStream, Encoding.UTF8);
        }

        protected override string GetProcessTitle() => "svn-diff";
    }
}
