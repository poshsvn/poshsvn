// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnLog")]
    [Alias("svn-log")]
    [OutputType(typeof(SvnLogOutput))]
    public class SvnLog : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true)]
        public SvnTarget[] Target { get; set; }

        [Parameter()]
        [Alias("rev", "r")]
        public SvnRevisionRange[] Revision { get; set; }

        [Parameter()]
        [Alias("v")]
        public SwitchParameter ChangedPaths { get; set; }

        [Parameter()]
        [Alias("l")]
        public int Limit { get; set; } = -1;

        [Parameter()]
        public SvnDepth Depth { get; set; }

        [Parameter()]
        [Alias("include-externals")]
        public SwitchParameter IncludeExternals { get; set; }

        [Parameter()]
        [Alias("with-all-revprops", "WithAllRevprops")]
        public SwitchParameter WithAllRevisionProperties { get; set; }

        [Parameter()]
        [Alias("with-no-revprops", "WithNoRevprops")]
        public SwitchParameter WithNoRevisionProperties { get; set; }

        [Parameter()]
        [Alias("with-revprop")]
        public string[] WithRevisionProperties { get; set; }

        public SvnLog()
        {
            Depth = SvnDepth.Empty;
            Target = new SvnTarget[]
            {
                SvnTarget.FromPath(".")
            };
            Revision = new SvnRevisionRange[]
            {
                new SvnRevisionRange("HEAD:0")
            };
        }

        protected override void Execute()
        {
            SvnLogArgs args = new SvnLogArgs
            {
                Limit = Limit,
                RetrieveChangedPaths = ChangedPaths,
                RetrieveAllProperties = WithAllRevisionProperties,
            };

            foreach (SvnRevisionRange range in Revision)
            {
                args.Ranges.Add(range.ToSharpSvnRevisionRange());
            }

            if (WithRevisionProperties != null)
            {
                args.RetrieveProperties.Clear();
                foreach (var property in WithRevisionProperties)
                {
                    args.RetrieveProperties.Add(property);
                }
            }

            if (WithNoRevisionProperties)
            {
                args.RetrieveProperties.Clear();
            }

            args.Progress += ProgressEventHandler;

            TargetCollection targets = TargetCollection.Parse(GetTargets(Target));

            targets.ThrowIfHasPathsAndUris();

            if (targets.HasPaths)
            {
                SvnClient.Log(targets.Paths, args, LogHandler);
            }
            else
            {
                SvnClient.Log(targets.Uris, args, LogHandler);
            }
        }

        private void LogHandler(object sender, SvnLogEventArgs e)
        {
            var obj = new SvnLogOutput
            {
                Revision = e.Revision,
                Author = e.Author,
                Message = e.LogMessage,
                ChangedPaths = ConvertSvnChangedPaths(e.ChangedPaths),
                RevisionProperties = e.RevisionProperties.ToArray(),
            };

            if (e.Time != DateTime.MinValue)
            {
                obj.Date = new DateTimeOffset(e.Time);
            }

            WriteObject(obj);

            UpdateProgressAction(string.Format("r{0}", e.Revision));
        }

        private SvnChangeItem[] ConvertSvnChangedPaths(KeyedCollection<string, SharpSvn.SvnChangeItem> source)
        {
            if (source == null)
            {
                return null;
            }
            else
            {
                SvnChangeItem[] result = new SvnChangeItem[source.Count];

                for (int i = 0; i < source.Count; i++)
                {
                    source[i].Detach();
                    result[i] = new SvnChangeItem(source[i]);
                }

                return result.ToArray();
            }
        }

        protected override string GetActivityTitle(SvnNotifyEventArgs e)
        {
            return "svn-log";
        }
    }
}
