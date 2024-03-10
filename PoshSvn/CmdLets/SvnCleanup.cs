// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnCleanup")]
    [Alias("svn-cleanup")]
    [OutputType(typeof(SvnCommitOutput))]
    public class SvnCleanup : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true)]
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

        protected override void Execute()
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
                        SvnClient.Vacuum(path, new SvnVacuumArgs
                        {
                            IncludeExternals = IncludeExternals,
                        });
                    }
                    else
                    {
                        SvnClient.CleanUp(path, new SvnCleanUpArgs
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
                        WriteSvnError(ex);
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
