using SharpSvn;
using System;
using System.Linq;
using System.Management.Automation;

namespace PoshSvn
{
    [Cmdlet("Invoke", "SvnCheckOut")]
    [Alias("svn-checkout")]
    [OutputType(typeof(SvnCheckOutOutput))]
    public class SvnCheckOut : SvnCmdletBase
    {
        [Parameter(Mandatory = true, Position = 0)]
        public Uri Url { get; set; }

        [Parameter(Position = 1)]
        public string Path { get; set; }

        [Parameter()]
        [Alias("r", "rev")]
        public SvnRevision Revision { get; set; }

        [Parameter()]
        [Alias("ignore-externals")]
        public SwitchParameter IgnoreExternals { get; set; }

        [Parameter()]
        [Alias("-f")]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                var args = new SvnCheckOutArgs
                {
                    Revision = Revision,
                    IgnoreExternals = IgnoreExternals,
                    //TODO: AllowObstructions = Force
                };

                ProgressRecord progress = new ProgressRecord(0, "Checking out", "Initializing...");

                args.Notify += new EventHandler<SvnNotifyEventArgs>((_, e) =>
                {
                    if (e.Action == SvnNotifyAction.UpdateStarted)
                    {
                        string text = string.Format("Checking out '{0}'", e.Path);
                        WriteVerbose(text);
                        progress.Activity = text;

                        WriteProgress(progress);
                    }
                    else if (e.Action == SvnNotifyAction.UpdateCompleted)
                    {
                        WriteObject(new SvnCheckOutOutput
                        {
                            Revision = e.Revision
                        });
                    }
                    else
                    {
                        string text = string.Format("{0,-5}{1}", SvnUtils.GetActionStringShort(e.Action), e.Path);
                        progress.StatusDescription = text;
                        WriteVerbose(text);
                        WriteProgress(progress);
                    }
                });
                args.Progress += new EventHandler<SvnProgressEventArgs>((_, e) =>
                {
                    progress.CurrentOperation = SvnUtils.FormatProgress(e.Progress);
                    WriteProgress(progress);
                });

                string resolvedPath;
                if (Path == null)
                {
                    resolvedPath = GetPathTarget(Url.Segments.Last());
                }
                else
                {
                    resolvedPath = GetPathTarget(Path);
                }

                client.CheckOut(new SvnUriTarget(Url), resolvedPath, args);
            }
        }
    }

    public class SvnCheckOutOutput
    {
        public long Revision { get; set; }
    }
}
