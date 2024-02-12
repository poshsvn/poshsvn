using SharpSvn;
using System;
using System.Management.Automation;
using System.Runtime.Remoting.Messaging;

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
                    ProgressRecord childProgress = new ProgressRecord(1, "Updating", "Loading...");

                    SvnUpdateArgs args = new SvnUpdateArgs
                    {
                        Revision = Revision,
                    };

                    int pathsCompletedCount = 0;

                    args.Notify += new EventHandler<SvnNotifyEventArgs>((sender, e) =>
                    {
                        if (e.Action == SvnNotifyAction.UpdateStarted)
                        {
                            WriteVerbose(string.Format("Updating '{0}':", e.Path));

                            WriteProgress(new ProgressRecord(0, "Updating", string.Format("Updating {0} of {1}", pathsCompletedCount, resolvedPaths.Length))
                            {
                                PercentComplete = pathsCompletedCount * 100 / resolvedPaths.Length
                            });

                            childProgress = new ProgressRecord(1, string.Format("Updating '{0}'", e.Path), "Updating")
                            {
                                ParentActivityId = 0
                            };
                            WriteProgress(childProgress);
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
                            childProgress.StatusDescription = string.Format("{0,-10} {1}", SvnUtils.GetActionStringLong(e.Action), e.Path);
                            WriteProgress(childProgress);
                        }
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
