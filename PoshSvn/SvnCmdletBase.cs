// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using PoshSvn.CmdLets;
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

        protected void NotifyEventHandler(object sender, SvnNotifyEventArgs e)
        {
            if (e.Action == SvnNotifyAction.UpdateStarted)
            {
                UpdateAction(GetActivityTitle(e));
            }
            else if (e.Action == SvnNotifyAction.UpdateCompleted)
            {
                if (e.CommandType == SvnCommandType.Update)
                {
                    WriteObject(new SvnUpdateOutput
                    {
                        Revision = e.Revision
                    });
                }
                else if (e.CommandType == SvnCommandType.CheckOut)
                {
                    WriteObject(new SvnCheckoutOutput
                    {
                        Revision = e.Revision
                    });
                }
                else if (e.CommandType == SvnCommandType.Switch)
                {
                    // TODO:
                    throw new NotImplementedException();
                }
            }
            else if (e.Action == SvnNotifyAction.CommitFinalizing)
            {
                UpdateAction("Committing transaction...");
            }
            else if (e.Action == SvnNotifyAction.CommitSendData)
            {
                UpdateAction("Transmitting file data...");
            }
            else
            {
                WriteObject(new SvnNotifyOutput
                {
                    Action = e.Action,
                    Path = e.Path
                });
            }
        }

        protected void ProgressEventHandler(object sender, SvnProgressEventArgs e)
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

        protected IEnumerable<object> GetTargets(string[] Target, string[] Path, Uri[] Url, bool resolved)
        {
            if (ParameterSetName == TargetParameterSetNames.Target)
            {
                foreach (string target in Target)
                {
                    if (target.Contains("://") && SvnUriTarget.TryParse(target, false, out _))
                    {
                        yield return new Uri(target);
                    }
                    else
                    {
                        foreach (string path in GetPathTargets(target, resolved))
                        {
                            // TODO: check providerInfo

                            yield return path;
                        }
                    }
                }
            }
            else if (ParameterSetName == TargetParameterSetNames.Path)
            {
                foreach (string path in GetPathTargets(Path, null))
                {
                    yield return path;
                }
            }
            else if (ParameterSetName == TargetParameterSetNames.Url)
            {
                foreach (Uri url in Url)
                {
                    yield return url;
                }
            }
        }

        protected object GetTarget(string Target, string Path, Uri Url)
        {
            if (ParameterSetName == TargetParameterSetNames.Target)
            {
                if (Target.Contains("://") && SvnUriTarget.TryParse(Target, false, out _))
                {
                    return new Uri(Target);
                }
                else
                {
                    return GetPathTarget(Target);
                }
            }
            else if (ParameterSetName == TargetParameterSetNames.Path)
            {
                return GetPathTarget(Path);
            }
            else if (ParameterSetName == TargetParameterSetNames.Url)
            {
                return Url;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected void CommittedEventHandler(object sender, SvnCommittedEventArgs e)
        {
            WriteObject(new SvnCommitOutput
            {
                Revision = e.Revision
            });
        }

        protected void CommittingEventHandler(object sender, SvnCommittingEventArgs e)
        {
            UpdateAction("Committing transaction...");
        }
    }
}
