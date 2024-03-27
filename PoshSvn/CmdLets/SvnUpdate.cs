// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnUpdate")]
    [Alias("svn-update")]
    [OutputType(typeof(SvnUpdateOutput))]
    public class SvnUpdate : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true)]
        public string[] Path { get; set; } = new string[] { "" };

        [Parameter()]
        [Alias("rev")]
        public SvnRevision Revision { get; set; } = null;

        protected override string GetActivityTitle(SvnNotifyEventArgs e)
        {
            return e == null ? "Updating" : string.Format("Updating '{0}'", e.Path);
        }

        protected override void Execute()
        {
            string[] resolvedPaths = GetPathTargets(Path, null);

            SvnUpdateArgs args = new SvnUpdateArgs
            {
                Revision = Revision,
            };

            SvnClient.Update(resolvedPaths, args);
        }
    }
}
