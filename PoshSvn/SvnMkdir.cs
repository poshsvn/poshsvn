using SharpSvn;
using System;
using System.Management.Automation;

namespace PoshSvn
{
    [Cmdlet("Invoke", "SvnMkdir", DefaultParameterSetName = "Path")]
    [Alias("svn-mkdir")]
    [OutputType(typeof(SvnMkdirLocalOutput))]
    public class SvnMkDir : SvnCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Path")]
        public string[] Path { get; set; }

        [Parameter(ParameterSetName = "Url", Mandatory = true)]
        public Uri[] Url { get; set; }

        [Parameter(ParameterSetName = "Url", Mandatory = true)]
        [Alias("m")]
        public string Message { get; set; }

        [Parameter()]
        public SwitchParameter Parents { get; set; }

        protected override string GetActivityTitle(SvnNotifyEventArgs e) => "Creating directory";

        protected override object GetNotifyOutput(SvnNotifyEventArgs e)
        {
            return new SvnMkdirLocalOutput
            {
                Action = e.Action,
                Path = e.Path
            };
        }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                if (Path != null)
                {
                    SvnCreateDirectoryArgs args = new SvnCreateDirectoryArgs
                    {
                        CreateParents = Parents,
                    };

                    string[] resolvedPaths = GetPathTargets(null, Path);
                    // TODO: maybe, I'll do it after
                    // int filesProcessedCount = 0;

                    args.Notify += Notify;
                    args.Progress += Progress;

                    client.CreateDirectories(resolvedPaths, args);
                }
                else
                {
                    SvnCreateDirectoryArgs args = new SvnCreateDirectoryArgs
                    {
                        CreateParents = Parents,
                        LogMessage = Message
                    };

                    args.Committing += new EventHandler<SvnCommittingEventArgs>((_, e) =>
                    {
                         UpdateAction("Committing transaction...");
                    });

                    args.Progress += Progress;

                    args.Committed += new EventHandler<SvnCommittedEventArgs>((_, e) =>
                    {
                        WriteObject(new SvnMkdirRemoteOutput
                        {
                            Revision = e.Revision
                        });
                    });

                    UpdateAction("Creating transaction...");
                    client.RemoteCreateDirectories(Url, args);
                }
            }
        }
    }

    public class SvnMkdirLocalOutput
    {
        public SvnNotifyAction Action { get; set; }
        public string ActionString => SvnUtils.GetActionStringShort(Action);
        public string Path { get; set; }
    }

    public class SvnMkdirRemoteOutput
    {
        public long Revision { get; set; }
    }
}
