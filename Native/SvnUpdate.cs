using SharpSvn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace SvnPosh
{
    [Cmdlet("Invoke", "SvnUpdate")]
    [Alias("svn-update")]
    [OutputType(typeof(SvnUpdateOutput))]
    public class SvnUpdate : PSCmdlet
    {
        [Parameter()]
        public SvnRevision Revision { get; set; } = null;

        [Parameter(ValueFromRemainingArguments = true)]
        public string[] Path { get; set; } = new string[] { "" };

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

        protected string[] GetPathTargets(string[] pathList, string[] literalPathList)
        {
            List<string> result = new List<string>();

            if (literalPathList != null)
            {
                foreach (string literalPath in literalPathList)
                {
                    string unresolvedPath = GetUnresolvedProviderPathFromPSPath(literalPath);
                    result.Add(unresolvedPath);
                }
            }
            else if (pathList != null)
            {
                foreach (string path in pathList)
                {
                    Collection<string> resolvedPath = GetResolvedProviderPathFromPSPath(path, out _);
                    result.AddRange(resolvedPath);
                }
            }

            return result.ToArray();
        }
    }

    public class SvnUpdateOutput
    {
        public long Revision { get; set; }
    }
}
