using SharpSvn;
using System;
using System.Diagnostics;
using System.Management.Automation;

namespace SvnPosh
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

        private string state = "Creating transaction...";
        private long progress = -1;

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

                    args.Notify += new EventHandler<SvnNotifyEventArgs>((_, e) =>
                    {
                        WriteObject(new SvnMkdirLocalOutput
                        {
                            Action = e.Action,
                            Path = e.Path
                        });
                    });

                    string[] resolvedPaths = GetPathTargets(null, Path);

                    client.CreateDirectories(resolvedPaths, args);
                }
                else
                {
                    SvnCreateDirectoryArgs args = new SvnCreateDirectoryArgs
                    {
                        CreateParents = Parents,
                        LogMessage = Message
                    };

                    WriteVerbose(state);

                    args.Committing += new EventHandler<SvnCommittingEventArgs>((_, e) =>
                    {
                        state = "Committing transaction...";
                        WriteVerbose(state);
                        UpdateProgress();
                    });

                    args.Progress += new EventHandler<SvnProgressEventArgs>((_, e) =>
                    {
                        progress = e.Progress;
                        UpdateProgress();
                    });

                    args.Committed += new EventHandler<SvnCommittedEventArgs>((_, e) =>
                    {
                        WriteObject(new SvnMkdirRemoteOutput
                        {
                            Revision = e.Revision
                        });
                    });

                    client.RemoteCreateDirectories(Url, args);
                }
            }
        }

        private void UpdateProgress()
        {
            WriteProgress(new ProgressRecord(0, state, SvnUtils.FormatBasicProgress(progress)));
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
