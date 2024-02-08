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
                    SvnUpdateArgs args = new SvnUpdateArgs
                    {
                        Revision = Revision,
                    };

                    int pathsCompletedCount = 0;

                    args.Notify += new EventHandler<SvnNotifyEventArgs>((sender, e) =>
                    {
                        if (e.Action == SvnNotifyAction.UpdateCompleted)
                        {
                            WriteObject(new SvnUpdateOutput
                            {
                                Revision = e.Revision
                            });

                            pathsCompletedCount++;
                        }
                        else if (e.Action == SvnNotifyAction.UpdateStarted)
                        {
                            WriteVerbose(string.Format("Updating '{0}':", e.Path));

                            WriteProgress(new ProgressRecord(0, "Updating", string.Format("Updating '{0}':", e.Path))
                            {
                                RecordType = ProgressRecordType.Processing,
                                PercentComplete = pathsCompletedCount * 100 / resolvedPaths.Length
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
