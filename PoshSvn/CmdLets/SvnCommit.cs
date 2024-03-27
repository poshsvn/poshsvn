// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Collections;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnCommit")]
    [Alias("svn-commit")]
    [OutputType(typeof(SvnCommitOutput))]
    public class SvnCommit : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true)]
        public string[] Path { get; set; } = new string[] { "" };

        [Parameter(Mandatory = true)]
        public string Message { get; set; }

        [Parameter()]
        [Alias("with-revprop", "rp", "revprop")]
        public Hashtable RevisionProperties { get; set; }

        protected override void Execute()
        {
            SvnCommitArgs args = new SvnCommitArgs
            {
                LogMessage = Message,
            };

            if (RevisionProperties != null)
            {
                foreach (var item in RevisionProperties)
                {
                    DictionaryEntry prop = (DictionaryEntry)item;
                    args.LogProperties.Add(prop.Key.ToString(), prop.Value.ToString());
                }
            }

            SvnClient.Commit(GetPathTargets(Path, null), args);
        }

        protected override string GetActivityTitle(SvnNotifyEventArgs e)
        {
            return "Committing";
        }
    }
}
