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
            ProgressRecord = new ProgressRecord(0, "TODO", "Initializing...");
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

        protected string GetPathTarget(string path)
        {
            return GetUnresolvedProviderPathFromPSPath(path);
        }

        protected void Notify(object sender, SvnNotifyEventArgs e)
        {
            if (e.Action == SvnNotifyAction.UpdateStarted)
            {
                string text = GetActivityTitle(e);
                WriteVerbose(text);
                ProgressRecord.Activity = text;
                WriteProgress(ProgressRecord);
            }
            else if (e.Action == SvnNotifyAction.UpdateCompleted)
            {
                WriteObject(GetOutput(e));
            }
            else
            {
                string text = string.Format("{0,-5}{1}", SvnUtils.GetActionStringShort(e.Action), e.Path);
                ProgressRecord.StatusDescription = text;
                WriteVerbose(text);
                WriteProgress(ProgressRecord);
            }
        }

        protected void Progress(object sender, SvnProgressEventArgs e)
        {
            ProgressRecord.CurrentOperation = SvnUtils.FormatProgress(e.Progress);
            WriteProgress(ProgressRecord);
        }

        // TODO: make them abstract
        protected virtual string GetActivityTitle(SvnNotifyEventArgs e)
        {
            return null;
        }

        protected virtual object GetOutput(SvnNotifyEventArgs e)
        {
            return null;
        }
    }
}
