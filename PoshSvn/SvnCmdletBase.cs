// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using SharpSvn;

namespace PoshSvn
{
    public abstract class SvnCmdletBase : PSCmdlet
    {
        protected ProgressRecord ProgressRecord;

        protected CancellationTokenSource cancellationTokenSource;
        protected CancellationToken cancellationToken;

        protected SvnCmdletBase()
        {
            ProgressRecord = new ProgressRecord(0, GetActivityTitle(null), "Initializing...");
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
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

        protected virtual string GetActivityTitle(SvnNotifyEventArgs e) => "Processing";

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

        protected string[] GetPathTargets(string[] paths)
        {
            try
            {
                return GetPathTargets(paths, true).ToArray();
            }
            catch (ItemNotFoundException)
            {
                return GetPathTargets(paths, false).ToArray();
            }
        }

        protected SvnResolvedTarget ResolveUriTarget(SvnTarget target)
        {
            if (Uri.TryCreate(target.Value, UriKind.Absolute, out Uri url))
            {
                return new SvnResolvedTarget(null, url, true, target.Revision);
            }
            else
            {
                throw new ArgumentException("Wrong Url format.", "Url");
            }
        }

        protected SvnResolvedTarget ResolveLiteralTarget(SvnTarget target)
        {
            string path = GetUnresolvedProviderPathFromPSPath(target.Value);
            return new SvnResolvedTarget(path, null, false, target.Revision);
        }

        protected ResolvedTargetCollection ResolveTargets(IEnumerable<SvnTarget> targets)
        {
            List<SvnResolvedTarget> resolvedTargets = new List<SvnResolvedTarget>();

            foreach (SvnTarget target in targets)
            {
                if (target.Type == SvnTargetType.Path)
                {
                    foreach (string path in GetPathTargets(new string[] { target.Value }))
                    {
                        resolvedTargets.Add(new SvnResolvedTarget(path, null, false, target.Revision));
                    }
                }
                else if (target.Type == SvnTargetType.LiteralPath)
                {
                    resolvedTargets.Add(ResolveLiteralTarget(target));
                }
                else if (target.Type == SvnTargetType.Url)
                {
                    resolvedTargets.Add(ResolveUriTarget(target));
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return new ResolvedTargetCollection(resolvedTargets);
        }

        protected SvnResolvedTarget ResolveTarget(SvnTarget target)
        {
            if (target.Type == SvnTargetType.Path)
            {
                return ResolveLiteralTarget(target);
            }
            else if (target.Type == SvnTargetType.LiteralPath)
            {
                return ResolveLiteralTarget(target);
            }
            else if (target.Type == SvnTargetType.Url)
            {
                return ResolveUriTarget(target);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected void SvnClient_Cancel(object sender, SharpSvn.SvnCancelEventArgs e)
        {
            e.Cancel = cancellationToken.IsCancellationRequested;
        }

        protected override void StopProcessing()
        {
            cancellationTokenSource.Cancel();

            base.StopProcessing();
        }
    }
}
