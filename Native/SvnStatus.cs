using SharpSvn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;

namespace SvnPosh
{
    [Cmdlet("Invoke", "SvnStatus")]
    [Alias("svn-status")]
    [OutputType(typeof(SvnStatusOutput))]
    public class SvnStatus : PSCmdlet
    {
        [Parameter()]
        public SwitchParameter All { get; set; }

        [Parameter(ValueFromRemainingArguments = true)]
        public string[] Path { get; set; } = new string[] { "" };

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                string[] resolvedPaths = GetPathTargets(Path, null);

                foreach (string resolvedPath in resolvedPaths)
                {
                    try
                    {
                        client.Status(
                            resolvedPath,
                            new SvnStatusArgs()
                            {
                                RetrieveAllEntries = All
                            },
                            new EventHandler<SvnStatusEventArgs>((sender, e) =>
                                {
                                    WriteObject(new SvnStatusOutput
                                    {
                                        Status = e.LocalNodeStatus,
                                        Path = e.Path
                                    });
                                }));
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

        protected string[] GetPathTargets(string[] pathList, string[] literalPathList)
        {
            List<string> result = new List<string>();
            if (literalPathList != null)
            {
                foreach (var literalPath in literalPathList)
                {
                    var unresolvedPath = GetUnresolvedProviderPathFromPSPath(literalPath);
                    result.Add(unresolvedPath);
                }
            }
            else if (pathList != null)
            {
                ProviderInfo provider;
                foreach (var path in pathList)
                {
                    var resolvedPath = GetResolvedProviderPathFromPSPath(path, out provider);
                    result.AddRange(resolvedPath);
                }
            }

            return result.ToArray();
        }
    }

    public class SvnStatusOutput
    {
        public SharpSvn.SvnStatus Status { get; set; }

        public string Path { get; set; }
    }
}
