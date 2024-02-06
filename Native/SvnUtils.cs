using System;

namespace SvnPosh
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
    }
}