// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Linq;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnBlame")]
    [Alias("svn-blame")]
    [OutputType(typeof(SvnBlameLine))]
    public class SvnBlameCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0)]
        public SvnTarget Target { get; set; }

        protected override void Execute()
        {
            SvnBlameArgs args = new SvnBlameArgs
            {
            };

            SharpSvn.SvnTarget target = TargetCollection.ConvertTargetToSvnTarget(GetTarget(Target));

            SvnClient.Blame(target, args, Blamer);
        }

        private void Blamer(object sender, SvnBlameEventArgs e)
        {
            WriteObject(new SvnBlameLine
            {
                Revision = e.Revision,
                Author = e.Author,
                LineNumber = e.LineNumber,
                Line = e.Line,

                EndRevision = e.EndRevision,
                LocalChange = e.LocalChange,
                Time = e.Time,
                MergedTime = e.MergedTime,
                MergedAuthor = e.MergedAuthor,
                MergedPath = e.MergedPath,
                MergedRevision = e.MergedRevision,
                MergedRevisionProperties = e.MergedRevisionProperties?.ToArray(),
            });
        }
    }
}
