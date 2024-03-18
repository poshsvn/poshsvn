// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using SharpSvn;

namespace PoshSvn
{
    public static class SvnRevisionParser
    {
        // Note: in subversion this function called 'parse_one_rev'
        public static SvnRevision ParseSvnRevision(string str)
        {
            int i = 0;

            while (i < str.Length && str[i] == 'r')
            {
                i++;
            }

            if (i < str.Length && str[i] == '{')
            {
                throw new NotImplementedException(); // TODO:
            }
            else if (long.TryParse(str.Substring(i), out long revisionNumber))
            {
                return new SvnRevision(revisionNumber);
            }
            else
            {
                string word = str.Substring(i).Trim();

                if (word.Equals("head", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new SvnRevision(SvnRevisionType.Head);
                }
                else if (word.Equals("prev", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new SvnRevision(SvnRevisionType.Previous);
                }
                else if (word.Equals("base", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new SvnRevision(SvnRevisionType.Base);
                }
                else if (word.Equals("committed", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new SvnRevision(SvnRevisionType.Committed);
                }
                else
                {
                    throw new ArgumentException("Cannot parse revision.");
                }
            }
        }
    }
}
