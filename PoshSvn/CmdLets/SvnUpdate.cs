using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnUpdate")]
    [Alias("svn-update")]
    [OutputType(typeof(SvnUpdateOutput))]
    public class SvnUpdate : SvnCmdletBase
    {
        [Parameter(Position = 0)]
        public string[] Path { get; set; } = new string[] { "" };

        [Parameter()]
        [Alias("rev")]
        public SvnRevision Revision { get; set; } = null;

        protected override string GetActivityTitle(SvnNotifyEventArgs e)
        {
            return e == null ? "Updating" : string.Format("Updating '{0}'", e.Path);
        }

        protected override object GetNotifyOutput(SvnNotifyEventArgs e)
        {
            return new SvnUpdateOutput
            {
                Revision = e.Revision
            };
        }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                string[] resolvedPaths = GetPathTargets(Path, null);

                try
                {
                    SvnUpdateArgs args = new SvnUpdateArgs
                    {
                        Revision = Revision,
                    };

                    args.Notify += Notify;
                    args.Progress += Progress;

                    client.Update(resolvedPaths, args);
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

    public class SvnUpdateOutput
    {
        public long Revision { get; set; }
    }
}
