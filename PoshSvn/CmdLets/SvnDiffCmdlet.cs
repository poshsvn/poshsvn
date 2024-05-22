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

        [Parameter(ParameterSetName = ParameterSetNames.Target)]
        [Alias("rev", "r")]
        public SvnRevisionRange Revision { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Target)]
        [Alias("c")]
        public SvnRevisionChange Change { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; }

        [Parameter()]
        public SwitchParameter AsByteStream { get; set; }

        [Parameter()]
        public SwitchParameter Raw { get; set; }

        [Parameter()]
        [ArgumentCompleter(typeof(EncodingArgumentCompletions))]
        [EncodingArgumentTransformation()]
        public Encoding Encoding { get; set; } = Encoding.UTF8;

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

        [Parameter()]
        [Alias("x")]
        public DiffExtension Extensions { get; set; }

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
            if (ParameterSetName == ParameterSetNames.Target)
            {
                SvnRevisionRangeBase svnRevision =
                    SvnRevisionUtils.CreateRangeFromRevisionOrChange(
                        Revision, Change, SvnRevisionUtils.WorkingChangesRange);

                SharpSvn.SvnRevisionRange sharpSvnRevision = svnRevision.ToSharpSvnRevisionRange();

                ResolvedTargetCollection resolvedTarget = ResolveTargets(Target);

                foreach (SharpSvn.SvnTarget target in resolvedTarget.EnumerateSharpSvnTargets())
                {
                    using (Stream stream = GetStream())
                    {
                        SharpSvn.SvnDiffArgs args = CreateSvnDiffArgs();

                        SvnClient.Diff(target, sharpSvnRevision, args, stream);
                    }
                }
            }
            else
            {
                using (Stream stream = GetStream())
                {
                    SharpSvn.SvnTarget oldTarget = ResolveTarget(Old).ConvertToSharpSvnTarget();
                    SharpSvn.SvnTarget newTarget = ResolveTarget(New).ConvertToSharpSvnTarget();
                    SharpSvn.SvnDiffArgs args = CreateSvnDiffArgs();

                    SvnClient.Diff(oldTarget, newTarget, args, stream);
                };
            }
        }

        protected SharpSvn.SvnDiffArgs CreateSvnDiffArgs()
        {
            SharpSvn.SvnDiffArgs args = new SharpSvn.SvnDiffArgs
            {
                Depth = Depth.ConvertToSharpSvnDepth(),
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

            foreach (string extension in Extensions.ConvertToArgumentCollection())
            {
                args.DiffArguments.Add(extension);
            }

            return args;
        }

        protected Stream GetStream()
        {
            if (AsByteStream)
            {
                return new ByteStream(this);
            }
            else
            {
                ITextStream textStream;

                if (Raw)
                {
                    textStream = new TextStream(this);
                }
                else
                {
                    ITextLineStream lineStream = new TextLineStream(this);
                    textStream = new LineDecoderTextStream(lineStream);
                }

                return new DecoderStream(textStream, Encoding);
            }
        }

        protected override string GetProcessTitle() => "svn-diff";
    }
}
