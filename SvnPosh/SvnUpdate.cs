using SharpSvn;
using System;
using System.Management.Automation;

namespace SvnPosh
{
    [Cmdlet("Invoke", "SvnUpdate")]
    [Alias("svn-update")]
    [OutputType(typeof(SvnUpdateOutput))]
    public class SvnUpdate : SvnCmdletBase
    {
        [Parameter(Position = 0)]
        public string[] Path { get; set; } = new string[] { "" };

        [Parameter()]
        [Alias("r", "rev")]
        public SvnRevision Revision { get; set; } = null;

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                string[] resolvedPaths = GetPathTargets(Path, null);

                try
                {
                    ProgressRecord progress = new ProgressRecord(0, "Updating", "Initializing...");

                    SvnUpdateArgs args = new SvnUpdateArgs
                    {
                        Revision = Revision,
                    };

                    int pathsCompletedCount = 0;

                    args.Notify += new EventHandler<SvnNotifyEventArgs>((sender, e) =>
                    {
                        if (e.Action == SvnNotifyAction.UpdateStarted)
                        {
                            string text = string.Format("Updating '{0}'", e.Path);
                            WriteVerbose(text);
                            progress.Activity = text;

                            WriteProgress(progress);
                        }
                        else if(e.Action == SvnNotifyAction.UpdateCompleted)
                        {
                            WriteObject(new SvnUpdateOutput
                            {
                                Revision = e.Revision
                            });

                            pathsCompletedCount++;
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

                    client.Update(resolvedPaths, args);
                }
                catch (SvnException ex)
                {
                    if (ex.ContainsError(SvnErrorCode.SVN_ERR_WC_NOT_WORKING_COPY,
                                         SvnErrorCode.SVN_ERR_WC_PATH_NOT_FOUND))
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
    }

    public class SvnUpdateOutput
    {
        public long Revision { get; set; }
    }
}
