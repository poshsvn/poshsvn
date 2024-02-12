﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace PoshSvn
{
    public abstract class SvnCmdletBase : PSCmdlet
    {
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
    }
}
