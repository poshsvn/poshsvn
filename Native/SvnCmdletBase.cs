using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace SvnPosh
{
    public abstract class SvnCmdletBase : PSCmdlet
    {
        [Parameter(ValueFromRemainingArguments = true)]
        public string[] Path { get; set; } = new string[] { "" };

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
}
