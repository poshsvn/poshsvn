using SharpSvn;
using System;
using System.Management.Automation;

namespace PoshSvn
{
    [Cmdlet("Invoke", "SvnInfo", DefaultParameterSetName = ParameterSetNames.Target)]
    [Alias("svn-info")]
    [OutputType(typeof(SvnInfoLocalOutput), typeof(SvnInfoRemoteOutput))]
    public class SvnInfo : SvnCmdletBase
    {
        public static class ParameterSetNames
        {
            public const string Target = "Target";
            public const string Path = "Path";
            public const string Url = "Url";
        }

        [Parameter(Position = 0, ParameterSetName = ParameterSetNames.Target, ValueFromRemainingArguments = true)]
        public string[] Target { get; set; } = new string[] { "" };

        [Parameter(ParameterSetName = ParameterSetNames.Path)]
        public string[] Path { get; set; }

        [Parameter(ParameterSetName = ParameterSetNames.Url)]
        public Uri[] Url { get; set; }

        [Parameter()]
        [Alias("r", "rev")]
        public SvnRevision Revision { get; set; } = null;

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Empty;

        [Parameter()]
        [Alias("include-externals")]
        public SwitchParameter IncludeExternals { get; set; }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    SvnInfoArgs args = new SvnInfoArgs
                    {
                        Revision = Revision,
                        IncludeExternals = IncludeExternals,
                        Depth = Depth.ConvertToSharpSvnDepth(),
                    };

                    args.Progress += Progress;

                    if (ParameterSetName == ParameterSetNames.Target)
                    {
                        foreach (string target in Target)
                        {
                            if (target.Contains("://") && SvnUriTarget.TryParse(target, true, out var uriTarget))
                            {
                                client.Info(uriTarget, args, InfoHandler);
                            }
                            else
                            {
                                foreach (string path in GetResolvedProviderPathFromPSPath(target, out _))
                                {
                                    if (SvnPathTarget.TryParse(path, true, out SvnPathTarget pathTarget))
                                    {
                                        client.Info(pathTarget, args, InfoHandler);
                                    }
                                }
                            }
                        }
                    }
                    else if (ParameterSetName == ParameterSetNames.Path)
                    {
                        foreach (string path in GetPathTargets(Path, null))
                        {
                            client.Info(SvnTarget.FromString(path), args, InfoHandler);
                        }
                    }
                    else if (ParameterSetName == ParameterSetNames.Url)
                    {
                        foreach (Uri url in Url)
                        {
                            client.Info(SvnTarget.FromUri(url), args, InfoHandler);
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                catch (SvnException ex)
                {
                    if (ex.ContainsError(SvnErrorCode.SVN_ERR_WC_NOT_WORKING_COPY))
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

        private void InfoHandler(object sender, SvnInfoEventArgs e)
        {
            if (e.HasLocalInfo)
            {
                SvnInfoLocalOutput svnInfo = new SvnInfoLocalOutput();
                FillSvnInfoOutputProperties(svnInfo, e);
                svnInfo.Schedule = e.Schedule;
                svnInfo.WorkingCopyRoot = e.WorkingCopyRoot;
                WriteObject(svnInfo);
            }
            else
            {
                SvnInfoRemoteOutput svnInfo = new SvnInfoRemoteOutput();
                FillSvnInfoOutputProperties(svnInfo, e);
                WriteObject(svnInfo);
            }
        }

        private void FillSvnInfoOutputProperties(SvnInfoOutput svnInfo, SvnInfoEventArgs e)
        {
            svnInfo.Path = e.Path;
            svnInfo.Url = e.Uri;
            svnInfo.RelativeUrl = e.RepositoryRoot.MakeRelativeUri(e.Uri);
            svnInfo.RepositoryRoot = e.RepositoryRoot;
            svnInfo.RepositoryId = e.RepositoryId;
            svnInfo.Revision = e.Revision;
            svnInfo.NodeKind = e.NodeKind;
            svnInfo.LastChangedAuthor = e.LastChangeAuthor;
            svnInfo.LastChangedRevision = e.LastChangeRevision;
            svnInfo.LastChangedDate = e.LastChangeTime;
        }
    }

    public abstract class SvnInfoOutput
    {
        public string Path { get; set; }
        public Uri Url { get; set; }
        public Uri RelativeUrl { get; set; }
        public Uri RepositoryRoot { get; set; }
        public Guid RepositoryId { get; set; }
        public long Revision { get; set; }
        public SvnNodeKind NodeKind { get; set; }
        public string LastChangedAuthor { get; set; }
        public long LastChangedRevision { get; set; }
        public DateTime LastChangedDate { get; set; }
    }

    public class SvnInfoLocalOutput : SvnInfoOutput
    {
        public SvnSchedule Schedule { get; set; }
        public string WorkingCopyRoot { get; set; }
    }

    public class SvnInfoRemoteOutput : SvnInfoOutput
    {
    }
}
