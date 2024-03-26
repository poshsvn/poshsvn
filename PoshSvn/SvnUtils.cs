// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using SharpSvn;

namespace PoshSvn
{
    public static class SvnUtils
    {
        public static char GetStatusCode(SharpSvn.SvnStatus status)
        {
            switch (status)
            {
                case SharpSvn.SvnStatus.None: return ' ';
                case SharpSvn.SvnStatus.Normal: return ' ';
                case SharpSvn.SvnStatus.Added: return 'A';
                case SharpSvn.SvnStatus.Missing: return '!';
                case SharpSvn.SvnStatus.Incomplete: return '!';
                case SharpSvn.SvnStatus.Deleted: return 'D';
                case SharpSvn.SvnStatus.Replaced: return 'R';
                case SharpSvn.SvnStatus.Modified: return 'M';
                case SharpSvn.SvnStatus.Conflicted: return 'C';
                case SharpSvn.SvnStatus.Obstructed: return '~';
                case SharpSvn.SvnStatus.Ignored: return 'I';
                case SharpSvn.SvnStatus.External: return 'X';
                case SharpSvn.SvnStatus.NotVersioned: return '?';
                default: return '?';
            }
        }

        public static SharpSvn.SvnStatus GetCombinedStatus(SharpSvn.SvnStatus nodeStatus,
                                                           SharpSvn.SvnStatus textStatus,
                                                           bool versioned,
                                                           bool conflicted)
        {
            if (nodeStatus == SharpSvn.SvnStatus.Conflicted &&
                !versioned &&
                conflicted)
            {
                return SharpSvn.SvnStatus.Missing;
            }
            else if (nodeStatus == SharpSvn.SvnStatus.Modified)
            {
                return textStatus;
            }
            else
            {
                return nodeStatus;
            }
        }

        public static long? ConvertRevision(long revision)
        {
            if (revision < 0)
            {
                return null;
            }
            else
            {
                return revision;
            }
        }

        public static DateTime? ConvertTime(DateTime time)
        {
            if (time == DateTime.MinValue)
            {
                return null;
            }
            else
            {
                return time;
            }
        }

        public static string GetActionStringShort(SvnNotifyAction action)
        {
            switch (action)
            {
                case SvnNotifyAction.UpdateAdd:
                case SvnNotifyAction.Add:
                case SvnNotifyAction.CommitAdded:
                case SvnNotifyAction.CommitAddCopy:
                    return "A";

                case SvnNotifyAction.UpdateDelete:
                case SvnNotifyAction.Delete:
                case SvnNotifyAction.CommitDeleted:
                    return "D";

                case SvnNotifyAction.CommitModified:
                    return "M";

                case SvnNotifyAction.UpdateUpdate:
                    return "U";

                case SvnNotifyAction.CommitReplaced:
                case SvnNotifyAction.CommitReplacedWithCopy:
                case SvnNotifyAction.UpdateReplace:
                    return "R";

                // case SvnNotifyAction.Conflict: return "C";
                // case SvnNotifyAction.UpdateMerge: return "G";
                // case SvnNotifyAction.UpdateExist: return "E";

                default:
                    return action.ToString();
            }
        }

        public static string FormatProgress(long progress)
        {
            if (progress == -1)
            {
                return "Loading...";
            }
            else
            {
                return string.Format("Transferred: {0} KB", progress / 1024);
            }
        }

        public static string GetChangeActionString(SvnChangeAction action)
        {
            switch (action)
            {
                case SvnChangeAction.None: return " ";
                case SvnChangeAction.Add: return "A";
                case SvnChangeAction.Delete: return "D";
                case SvnChangeAction.Modify: return "M";
                case SvnChangeAction.Replace: return "R";
                default: throw new NotImplementedException();
            }
        }

        internal static char GetPropetyStatusString(SvnStatus localPropertyStatus, SvnStatus localNodeStatus)
        {
            if (localNodeStatus == SvnStatus.Added)
            {
                return ' ';
            }
            else if (localPropertyStatus == SvnStatus.Conflicted)
            {
                return 'C';
            }
            else if (localPropertyStatus == SvnStatus.Modified)
            {
                return 'M';
            }
            else
            {
                return ' ';
            }
        }

        internal static char GetLockedString(SvnLockInfo lockInfo)
        {
            if (lockInfo == null)
            {
                return ' ';
            }
            else
            {
                return 'L';
            }
        }

        internal static char GetCopiedString(bool localCopied)
        {
            if (localCopied)
            {
                return '+';
            }
            else
            {
                return ' ';
            }
        }
    }
}