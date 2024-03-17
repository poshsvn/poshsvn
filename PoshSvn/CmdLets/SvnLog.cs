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
        public SvnRevision Start { get; set; } = null;

        [Parameter()]
        public SvnRevision End { get; set; } = null;

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
        }

        protected override void Execute()
        {
            SvnLogArgs args = new SvnLogArgs
            {
                Limit = Limit,
                RetrieveChangedPaths = ChangedPaths,
                Start = Start,
                End = End,
                RetrieveAllProperties = WithAllRevisionProperties,
            };

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

            UpdateAction(string.Format("r{0}", e.Revision));
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

    public class SvnLogOutput
    {
        public long Revision { get; set; }
        public string Author { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string Message { get; set; }
        public SvnChangeItem[] ChangedPaths { get; set; }
        public SvnPropertyValue[] RevisionProperties { get; set; }
    }

    public class SvnChangeItem
    {
        public SvnChangeItem()
        {
        }

        internal SvnChangeItem(SharpSvn.SvnChangeItem source)
        {
            PropertiesModified = source.PropertiesModified;
            ContentModified = source.ContentModified;
            NodeKind = source.NodeKind;
            CopyFromRevision = source.CopyFromRevision;
            CopyFromPath = source.CopyFromPath;
            Action = source.Action.ToPoshSvnChangeAction();
            Path = source.Path;
        }

        public bool? PropertiesModified { get; set; }
        public bool? ContentModified { get; set; }
        public SvnNodeKind NodeKind { get; set; } // TODO:
        public long? CopyFromRevision { get; set; }
        public string CopyFromPath { get; set; }
        public SvnChangeAction Action { get; set; }
        public string ActionString => SvnUtils.GetChangeActionString(Action);
        public string Path { get; set; }
    }

    public enum SvnChangeAction
    {
        None,
        Add,
        Delete,
        Modify,
        Replace,
    }

    public static class SvnChangeActionExtensions
    {
        public static SvnChangeAction ToPoshSvnChangeAction(this SharpSvn.SvnChangeAction changeAction)
        {
            switch (changeAction)
            {
                case SharpSvn.SvnChangeAction.None: return SvnChangeAction.None;
                case SharpSvn.SvnChangeAction.Add: return SvnChangeAction.Add;
                case SharpSvn.SvnChangeAction.Delete: return SvnChangeAction.Delete;
                case SharpSvn.SvnChangeAction.Modify: return SvnChangeAction.Modify;
                case SharpSvn.SvnChangeAction.Replace: return SvnChangeAction.Replace;
                default: throw new NotImplementedException();
            }
        }
    }
}
