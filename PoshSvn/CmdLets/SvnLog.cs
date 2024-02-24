using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnLog", DefaultParameterSetName = TargetParameterSetNames.Target)]
    [Alias("svn-log", "Get-SvnLog")]
    [OutputType(typeof(SvnLogOutput))]
    public class SvnLog: SvnCmdletBase
    {
        [Parameter(Position = 0, ParameterSetName = TargetParameterSetNames.Target, ValueFromRemainingArguments = true)]
        public string[] Target { get; set; } = new string[] { "" };

        [Parameter(ParameterSetName = TargetParameterSetNames.Path)]
        public string[] Path { get; set; }

        [Parameter(ParameterSetName = TargetParameterSetNames.Url)]
        public Uri[] Url { get; set; }

        [Parameter()]
        [Alias("rev")]
        public SvnRevision Revision { get; set; } = null;

        [Parameter()]
        [Alias("l")]
        public int Limit { get; set; } = -1;

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Empty;

        [Parameter()]
        [Alias("include-externals")]
        public SwitchParameter IncludeExternals { get; set; }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    SvnLogArgs args = new SvnLogArgs
                    {
                        Limit = Limit,
                    };

                    args.Progress += ProgressEventHandler;

                    TargetCollection targets = TargetCollection.Parse(GetTargets(Target, Path, Url, true));

                    targets.ThrowIfHasPathsAndUris();

                    if (targets.HasPaths)
                    {
                        client.Log(targets.Paths, args, LogHandler);
                    }
                    else
                    {
                        client.Log(targets.Uris, args, LogHandler);
                    }
                }
                catch (SvnException ex)
                {
                    if (ex.ContainsError(SvnErrorCode.SVN_ERR_WC_NOT_WORKING_COPY))
                    {
                        WriteWarning(ex.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private void LogHandler(object sender, SvnLogEventArgs e)
        {
            var obj = new SvnLogOutput
            {
                Revision = e.Revision,
                Author = e.Author,
                Message = e.LogMessage
            };

            if (e.Time != DateTime.MinValue)
            {
                obj.Date = new DateTimeOffset(e.Time);
            }

            WriteObject(obj);

            UpdateAction(string.Format("r{0}", e.Revision));
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
    }
}
