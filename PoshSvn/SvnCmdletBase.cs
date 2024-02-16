using SharpSvn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace PoshSvn
{
    public abstract class SvnCmdletBase : PSCmdlet
    {
        protected ProgressRecord ProgressRecord;

        protected SvnCmdletBase()
        {
            ProgressRecord = new ProgressRecord(0, GetActivityTitle(null), "Initializing...");
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
                    Collection<string> resolvedPath = GetResolvedProviderPathFromPSPath(path, out ProviderInfo providerInfo);
                    // TODO: check providerInfo
                    result.AddRange(resolvedPath);
                }
            }

            return result.ToArray();
        }

        protected string GetPathTarget(string path)
        {
            return GetUnresolvedProviderPathFromPSPath(path);
        }

        protected void Notify(object sender, SvnNotifyEventArgs e)
        {
            if (e.Action == SvnNotifyAction.UpdateStarted)
            {
                UpdateAction(GetActivityTitle(e));
            }
            else if (e.Action == SvnNotifyAction.UpdateCompleted ||
                     e.Action == SvnNotifyAction.Add)
            {
                WriteObject(GetNotifyOutput(e));
            }
            else if (e.Action == SvnNotifyAction.CommitFinalizing)
            {
                UpdateAction("Committing transaction...");
            }
            else if (e.Action == SvnNotifyAction.CommitSendData)
            {
                UpdateAction("Transmitting file data...");
            }
            else if (e.Action == SvnNotifyAction.CommitAdded ||
                     e.Action == SvnNotifyAction.CommitAddCopy ||
                     e.Action == SvnNotifyAction.CommitAddCopy ||
                     e.Action == SvnNotifyAction.CommitDeleted ||
                     e.Action == SvnNotifyAction.CommitModified ||
                     e.Action == SvnNotifyAction.CommitReplaced ||
                     e.Action == SvnNotifyAction.CommitReplacedWithCopy)
            {
                UpdateAction(string.Format("{0,-8} {1}", SvnUtils.GetCommitActionString(e.Action), e.Path));
            }
            else
            {
                UpdateAction(string.Format("{0,-5}{1}", SvnUtils.GetActionStringShort(e.Action), e.Path));
            }
        }

        protected void Progress(object sender, SvnProgressEventArgs e)
        {
            ProgressRecord.CurrentOperation = SvnUtils.FormatProgress(e.Progress);
            WriteProgress(ProgressRecord);
        }

        protected virtual string GetActivityTitle(SvnNotifyEventArgs e) => "Processing";
        protected virtual object GetNotifyOutput(SvnNotifyEventArgs e) => null;

        protected void UpdateAction(string action)
        {
            WriteVerbose(action);
            ProgressRecord.StatusDescription = action;
            WriteProgress(ProgressRecord);
        }

        protected List<SvnTarget> GetTargets(string[] Target, string[] Path, Uri[] Url)
        {
            List<SvnTarget> result = new List<SvnTarget>();

            if (ParameterSetName == TargetParameterSetNames.Target)
            {
                foreach (string target in Target)
                {
                    if (target.Contains("://") && SvnUriTarget.TryParse(target, true, out var uriTarget))
                    {
                        result.Add(uriTarget);
                    }
                    else
                    {
                        foreach (string path in GetResolvedProviderPathFromPSPath(target, out ProviderInfo providerInfo))
                        {
                            // TODO: check providerInfo

                            if (SvnPathTarget.TryParse(path, true, out SvnPathTarget pathTarget))
                            {
                                result.Add(pathTarget);
                            }
                        }
                    }
                }
            }
            else if (ParameterSetName == TargetParameterSetNames.Path)
            {
                foreach (string path in GetPathTargets(Path, null))
                {
                    result.Add(SvnTarget.FromString(path));
                }
            }
            else if (ParameterSetName == TargetParameterSetNames.Url)
            {
                foreach (Uri url in Url)
                {
                    result.Add(SvnTarget.FromUri(url));
                }
            }

            return result;
        }
    }
}
