using System;
using System.Linq;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
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
        [Alias("rev")]
        public SvnRevision Revision { get; set; }

        [Parameter()]
        [Alias("ignore-externals")]
        public SwitchParameter IgnoreExternals { get; set; }

        [Parameter()]
        [Alias("f")]
        public SwitchParameter Force { get; set; }

        protected override string GetActivityTitle(SvnNotifyEventArgs e)
        {
            return e == null ? "Checking out" : string.Format("Checking out '{0}'", e.Path);
        }

        protected override object GetNotifyOutput(SvnNotifyEventArgs e)
        {
            return new SvnCheckOutOutput
            {
                Revision = e.Revision
            };
        }

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

                args.Notify += NotifyEventHandler;
                args.Progress += ProgressEventHandler;

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
