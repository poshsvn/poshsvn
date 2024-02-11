using SharpSvn;
using System;
using System.Linq;
using System.Management.Automation;

namespace SvnPosh
{
    [Cmdlet("Invoke", "SvnCheckOut")]
    [Alias("svn-checkout", "svn-co")]
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

        private long progress = 0;

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

                args.Notify += new EventHandler<SvnNotifyEventArgs>((_, e) =>
                {
                    if (e.Action == SvnNotifyAction.UpdateStarted)
                    {
                        WriteParrentProgress();
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
                        WriteVerbose(string.Format("{0,-5}{1}", SvnUtils.GetActionStringShort(e.Action), e.Path));
                        WriteProgress(new ProgressRecord(1, SvnUtils.GetActionStringLong(e.Action), e.Path)
                        {
                            ParentActivityId = 0
                        });
                    }
                });
                args.Progress += new EventHandler<SvnProgressEventArgs>((_, e) =>
                {
                    progress = e.Progress;
                    WriteParrentProgress();
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

        private void WriteParrentProgress()
        {
            WriteProgress(new ProgressRecord(0, "Checking out", string.Format("Transfered {0,10} kb", progress / 1024)));
        }
    }

    public class SvnCheckOutOutput
    {
        public long Revision { get; set; }
    }
}
