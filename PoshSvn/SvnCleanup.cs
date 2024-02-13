using SharpSvn;
using System;
using System.Management.Automation;

namespace PoshSvn
{
    [Cmdlet("Invoke", "SvnCleanup")]
    [Alias("svn-cleanup")]
    [OutputType(typeof(SvnCommitOutput))]
    public class SvnCleanup : SvnCmdletBase
    {
        [Parameter(Position = 0)]
        public string[] Path { get; set; } = new string[] { "" };

        [Parameter()]
        [Alias("remove-unversioned")]
        public SwitchParameter RemoveUnversioned { get; set; }

        [Parameter()]
        [Alias("remove-ignored")]
        public SwitchParameter RemoveIgnored { get; set; }

        [Parameter()]
        [Alias("vacuum-pristines")]
        public SwitchParameter VacuumPristines { get; set; }

        [Parameter()]
        [Alias("include-externals")]
        public SwitchParameter IncludeExternals { get; set; }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                foreach (var path in GetPathTargets(Path, null))
                {
                    try
                    {
                        if (RemoveUnversioned || RemoveIgnored || VacuumPristines)
                        {
                            // SharpSvn doesn't implement all arguments
                            // TODO: Add them ?
                            // https://github.com/AmpScm/SharpSvn/blob/main/src/SharpSvn/Commands/Vacuum.cpp
                            client.Vacuum(path, new SvnVacuumArgs
                            {
                                IncludeExternals = IncludeExternals,
                            });
                        }
                        else
                        {
                            client.CleanUp(path, new SvnCleanUpArgs
                            {
                                BreakLocks = true,
                                FixTimestamps = true,
                                ClearDavCache = true,
                                VacuumPristines = VacuumPristines,
                                IncludeExternals = IncludeExternals,
                            });
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
        }
    }
}
