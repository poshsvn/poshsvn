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

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                if (Path != null)
                {
                    ProgressRecord progressRecord = new ProgressRecord(0, "Creating directory", "Initializing...");

                    SvnCreateDirectoryArgs args = new SvnCreateDirectoryArgs
                    {
                        CreateParents = Parents,
                    };

                    string[] resolvedPaths = GetPathTargets(null, Path);
                    int filesProcessedCount = 0;

                    args.Notify += new EventHandler<SvnNotifyEventArgs>((_, e) =>
                    {
                        WriteObject(new SvnMkdirLocalOutput
                        {
                            Action = e.Action,
                            Path = e.Path
                        });

                        progressRecord.PercentComplete = 100 * filesProcessedCount / resolvedPaths.Length;
                        progressRecord.StatusDescription = e.Path;

                        WriteProgress(progressRecord);

                        filesProcessedCount++;
                    });

                    client.CreateDirectories(resolvedPaths, args);
                }
                else
                {
                    ProgressRecord progressRecord = new ProgressRecord(0, "Creating directory", "Creating transaction...");

                    SvnCreateDirectoryArgs args = new SvnCreateDirectoryArgs
                    {
                        CreateParents = Parents,
                        LogMessage = Message
                    };

                    args.Committing += new EventHandler<SvnCommittingEventArgs>((_, e) =>
                    {
                        progressRecord.StatusDescription = "Committing transaction...";
                        WriteVerbose("Committing transaction...");
                    });

                    args.Progress += new EventHandler<SvnProgressEventArgs>((_, e) =>
                    {
                        progressRecord.CurrentOperation = SvnUtils.FormatProgress(e.Progress);
                        WriteProgress(progressRecord);
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
