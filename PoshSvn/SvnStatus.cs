using SharpSvn;
using System;
using System.Management.Automation;

namespace SvnPosh
{
    [Cmdlet("Invoke", "SvnStatus")]
    [Alias("svn-status")]
    [OutputType(typeof(SvnStatusOutput))]
    public class SvnStatus : SvnCmdletBase
    {
        [Parameter(Position = 0)]
        public string[] Path { get; set; } = new string[] { "" };

        [Parameter()]
        public SwitchParameter All { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Infinity;

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
                                RetrieveAllEntries = All,
                                Depth = Depth.ConvertToSharpSvnDepth(),
                            },
                            new EventHandler<SvnStatusEventArgs>((sender, e) =>
                                {
                                    WriteObject(new SvnStatusOutput
                                    {
                                        LocalNodeStatus = e.LocalNodeStatus,
                                        LocalTextStatus = e.LocalTextStatus,
                                        Versioned = e.Versioned,
                                        Conflicted = e.Conflicted,
                                        LocalCopied = e.LocalCopied,
                                        Path = e.Path,
                                        LastChangedAuthor = e.LastChangeAuthor,
                                        LastChangedRevision = SvnUtils.ConvertRevision(e.LastChangeRevision),
                                        LastChangedTime = SvnUtils.ConvertTime(e.LastChangeTime),
                                        Revision = SvnUtils.ConvertRevision(e.Revision)
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
    }

    public class SvnStatusOutput
    {
        public SharpSvn.SvnStatus LocalNodeStatus { get; set; }
        public string Status
        {
            get
            {
                SharpSvn.SvnStatus combinedStatus =
                    SvnUtils.GetCombinedStatus(LocalNodeStatus,
                                               LocalTextStatus,
                                               Versioned,
                                               Conflicted);

                char statusChar = SvnUtils.GetStatusCode(combinedStatus);

                return new string(new char[] { statusChar });
            }
        }

        public string Path { get; set; }
        public SharpSvn.SvnStatus LocalTextStatus { get; set; }
        public bool Versioned { get; set; }
        public bool Conflicted { get; set; }
        public bool LocalCopied { get; set; }
        public long? Revision { get; set; }

        public long? LastChangedRevision { get; set; }
        public string LastChangedAuthor { get; set; }
        public DateTime? LastChangedTime { get; set; }
    }
}
