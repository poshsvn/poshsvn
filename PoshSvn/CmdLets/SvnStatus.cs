// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnStatus", DefaultParameterSetName = ParameterSetNames.Local)]
    [Alias("svn-status", "svn-st")]
    [OutputType(typeof(SvnLocalStatusOutput))]
    public class SvnStatus : SvnClientCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true)]
        public string[] Path { get; set; } = new string[] { "" };

        [Parameter(ParameterSetName = ParameterSetNames.Remote, Mandatory = true)]
        public SwitchParameter ShowUpdates { get; set; }

        [Parameter()]
        public SwitchParameter All { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Infinity;

        [Parameter(ParameterSetName = ParameterSetNames.Remote)]
        [Alias("rev")]
        public SharpSvn.SvnRevision Revision { get; set; }

        protected override void Execute()
        {
            string[] resolvedPaths = GetPathTargets(Path, null);

            foreach (string resolvedPath in resolvedPaths)
            {
                try
                {
                    SvnStatusArgs args = new SvnStatusArgs()
                    {
                        RetrieveAllEntries = All,
                        Depth = Depth.ConvertToSharpSvnDepth(),
                        RetrieveRemoteStatus = ShowUpdates
                    };

                    SvnClient.Status(resolvedPath, args, StatusHandler);
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

        private void StatusHandler(object sender, SvnStatusEventArgs e)
        {
            if (ShowUpdates)
            {
                WriteObject(new SvnRemoteStatusOutput
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
                    Revision = SvnUtils.ConvertRevision(e.Revision),

                    RemoteContentStatus = e.RemoteContentStatus,
                    RemoteLock = e.RemoteLock,
                    RemoteNodeStatus = e.RemoteNodeStatus,
                    RemoteTextStatus = e.RemoteTextStatus,
                    RemotePropertyStatus = e.RemotePropertyStatus,
                    RemoteUpdateCommitAuthor = e.RemoteUpdateCommitAuthor,
                    RemoteUpdateCommitTime = e.RemoteUpdateCommitTime,
                    RemoteUpdateNodeKind = e.RemoteUpdateNodeKind.ToPoshSvnNodeKind(),
                    RemoteUpdateRevision = e.RemoteUpdateRevision
                });
            }
            else
            {
                WriteObject(new SvnLocalStatusOutput
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
                    Revision = SvnUtils.ConvertRevision(e.Revision),
                    LocalPropertyStatus = e.LocalPropertyStatus,
                    LocalLock = e.LocalLock,
                });
            }
        }

        protected override string GetProcessTitle() => "svn-status";
    }

    public class SvnLocalStatusOutput
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
                char propStatus = SvnUtils.GetPropetyStatusString(LocalPropertyStatus, LocalNodeStatus);
                char locked = SvnUtils.GetLockedString(LocalLock);
                char copied = SvnUtils.GetCopiedString(LocalCopied);

                return new string(new char[] { statusChar, propStatus, locked, copied });
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
        public SharpSvn.SvnStatus LocalPropertyStatus { get; set; } = SharpSvn.SvnStatus.None;
        public SvnLockInfo LocalLock { get; set; }
    }

    public class SvnRemoteStatusOutput : SvnLocalStatusOutput
    {
        public SharpSvn.SvnStatus RemoteContentStatus { get; set; }
        public SharpSvn.SvnStatus RemoteNodeStatus { get; set; }
        public SharpSvn.SvnStatus RemoteTextStatus { get; set; }
        public SharpSvn.SvnStatus RemotePropertyStatus { get; set; }
        public SvnLockInfo RemoteLock { get; set; }
        public long RemoteUpdateRevision { get; set; }
        public DateTime RemoteUpdateCommitTime { get; set; }
        public string RemoteUpdateCommitAuthor { get; set; }
        public SvnNodeKind RemoteUpdateNodeKind { get; set; }
    }
}
