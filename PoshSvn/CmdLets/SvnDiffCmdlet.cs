// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.Concurrent;
using System.IO;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnDiff", DefaultParameterSetName = ParameterSetNames.Target)]
    [Alias("svn-diff")]
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
        public SwitchParameter NoDiffAdded {  get; set; }

        [Parameter()]
        public SwitchParameter NoDiffDeleted {  get; set; }

        [Parameter()]
        public SwitchParameter IgnoreProperties { get; set; }

        [Parameter()]
        public SwitchParameter PropertiesOnly { get;  set; }

        [Parameter()]
        public SwitchParameter ShowCopiesAsAdds { get; set; }

        [Parameter()]
        public SwitchParameter NoticeAncestry {  get; set; }

        [Parameter()]
        [Alias("cl")]
        public string Changelist { get; set; }

        [Parameter()]
        public SwitchParameter Git { get; set; }

        [Parameter()]
        public SwitchParameter PatchCompatible {  get; set; }

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
            SharpSvn.SvnRevision startRevision = new SharpSvn.SvnRevision(SharpSvn.SvnRevisionType.Base);
            SharpSvn.SvnRevision endRevision = new SharpSvn.SvnRevision(SharpSvn.SvnRevisionType.Working);
            SharpSvn.SvnRevisionRange rangeRevision = new SharpSvn.SvnRevisionRange(startRevision, endRevision);

            BlockingCollection<string> output = new BlockingCollection<string>(100);

            SharpSvn.SvnDiffArgs args = new SharpSvn.SvnDiffArgs
            {
                Depth = Depth.ConvertToSharpSvnDepth(),
                NoAdded = NoDiffAdded,
                NoDeleted = NoDiffDeleted,
                NoProperties = IgnoreProperties || PatchCompatible,
                PropertiesOnly = PropertiesOnly,
                CopiesAsAdds = ShowCopiesAsAdds || PatchCompatible,
                IgnoreAncestry = NoticeAncestry,
                UseGitFormat = Git,
            };

            if (Changelist != null)
            {
                args.ChangeLists.Add(Changelist);
            }

            if (ParameterSetName == ParameterSetNames.Target)
            {
                TargetCollection targets = TargetCollection.Parse(GetTargets(Target));

                foreach (SharpSvn.SvnTarget target in targets.Targets)
                {
                    using (Stream stream = GetStream(output))
                    {
                        Task task = Task.Run(() =>
                        {
                            try
                            {
                                SvnClient.Diff(target, rangeRevision, args, stream);
                            }
                            finally
                            {
                                output.CompleteAdding();
                            }
                        });

                        while (!output.IsCompleted)
                        {
                            if (output.TryTake(out string line))
                            {
                                WriteObject(line);
                            }
                        }

                        try
                        {
                            task.Wait();
                        }
                        catch (AggregateException ex)
                        {
                            throw ex.InnerException;
                        }
                    }
                }
            }
            else
            {
                using (Stream stream = GetStream(output))
                {
                    Task task = Task.Run(() =>
                    {
                        try
                        {
                            SvnClient.Diff(
                                TargetCollection.ConvertTargetToSvnTarget(GetTarget(Old)),
                                TargetCollection.ConvertTargetToSvnTarget(GetTarget(New)),
                                args, stream);
                        }
                        finally
                        {
                            output.CompleteAdding();
                        }
                    });

                    while (!output.IsCompleted)
                    {
                        if (output.TryTake(out string line))
                        {
                            WriteObject(line);
                        }
                    }

                    try
                    {
                        task.Wait();
                    }
                    catch (AggregateException ex)
                    {
                        throw ex.InnerException;
                    }
                }
            }
        }

        protected Stream GetStream(BlockingCollection<string> output)
        {
            ITextLineStream textStream = new TextLineStream(output);
            ITextStream lineStream = new LineDecoderTextStream(textStream);

            return new DecoderStream(lineStream, Encoding.UTF8);
        }

        private class TextLineStream : ITextLineStream
        {
            private readonly BlockingCollection<string> output;

            public TextLineStream(BlockingCollection<string> output)
            {
                this.output = output;
            }

            public void WriteLine(string line)
            {
                output.Add(line);
            }
        }
    }
}
