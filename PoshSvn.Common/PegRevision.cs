// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Text;

namespace PoshSvn
{
    public static class PegRevision
    {
        public static void ParsePegRevisionTarget(string target, out string remainingTarget, out SvnRevision revision)
        {
            int i = 0;

            StringBuilder remainingTargetSb = new StringBuilder();
            for (; i < target.Length; i++)
            {
                if (target[i] == '@')
                {
                    i++; // Skip '@'
                    break;
                }
                else
                {
                    remainingTargetSb.Append(target[i]);
                }
            }
            remainingTarget = remainingTargetSb.ToString();

            if (i < target.Length)
            {
                // We have revision

                StringBuilder revisionSb = new StringBuilder();

                for (; i < target.Length; i++)
                {
                    revisionSb.Append(target[i]);
                }

                revision = new SvnRevision(revisionSb.ToString());
            }
            else
            {
                // We don't have any more symbols, so peg-revision is not set.

                revision = null;
            }
        }
    }
}
