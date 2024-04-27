// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using SharpSvn;

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

        protected IEnumerable<string> GetPathTargets(string path, bool resolved)
        {
            if (resolved)
            {
                foreach (string resolvedPath in GetResolvedProviderPathFromPSPath(path, out ProviderInfo providerInfo))
                {
                    // TODO: check providerInfo
                    yield return resolvedPath;
                }
            }
            else
            {
                yield return GetUnresolvedProviderPathFromPSPath(path);
            }
        }

        protected IEnumerable<string> GetPathTargets(string[] paths, bool resolved)
        {
            if (resolved)
            {
                foreach (string path in paths)
                {
                    foreach (string resolvedPath in GetResolvedProviderPathFromPSPath(path, out ProviderInfo providerInfo))
                    {
                        // TODO: check providerInfo
                        yield return resolvedPath;
                    }
                }
            }
            else
            {
                foreach (string path in paths)
                {
                    yield return GetUnresolvedProviderPathFromPSPath(path);
                }
            }
        }

        protected string GetPathTarget(string path)
        {
            return GetUnresolvedProviderPathFromPSPath(path);
        }

        protected void ProgressEventHandler(object sender, SvnProgressEventArgs e)
        {
            ProgressRecord.CurrentOperation = SvnUtils.FormatProgress(e.Progress);
            WriteProgress(ProgressRecord);
        }

        protected virtual string GetActivityTitle(SvnNotifyEventArgs e) => "Processing";
        protected virtual object GetNotifyOutput(SvnNotifyEventArgs e) => null;

        protected void UpdateProgressAction(string action)
        {
            WriteVerbose(action);
            ProgressRecord.StatusDescription = action;
            WriteProgress(ProgressRecord);
        }

        protected void UpdateProgressTitile(string title)
        {
            ProgressRecord.Activity = title;
            WriteProgress(ProgressRecord);
        }

        protected string[] GetPathTargets(string path)
        {
            try
            {
                return GetPathTargets(path, true).ToArray();
            }
            catch (ItemNotFoundException)
            {
                return GetPathTargets(path, false).ToArray();
            }
        }

        protected IEnumerable<string> GetPathTargets(string[] paths)
        {
            foreach (string path in paths)
            {
                foreach (string resolvedPath in GetPathTargets(path))
                {
                    yield return resolvedPath;
                }
            }
        }

        protected IEnumerable<object> GetTargets(SvnTarget[] targets)
        {
            foreach (SvnTarget target in targets)
            {
                if (target.Type == SvnTargetType.Path)
                {
                    foreach (string path in GetPathTargets(target.Value))
                    {
                        yield return path;
                    }
                }
                else if (target.Type == SvnTargetType.LiteralPath)
                {
                    yield return GetUnresolvedProviderPathFromPSPath(target.Value);
                }
                else if (target.Type == SvnTargetType.Url)
                {
                    if (Uri.TryCreate(target.Value, UriKind.Absolute, out Uri uri))
                    {
                        yield return uri;
                    }
                    else
                    {
                        throw new ArgumentException("Wrong Url format.", "Url");
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        protected object GetTarget(SvnTarget target)
        {
            if (target.Type == SvnTargetType.Path)
            {
                return GetUnresolvedProviderPathFromPSPath(target.Value);
            }
            else if (target.Type == SvnTargetType.LiteralPath)
            {
                return GetUnresolvedProviderPathFromPSPath(target.Value);
            }
            else if (target.Type == SvnTargetType.Url)
            {
                return new Uri(target.Value);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
