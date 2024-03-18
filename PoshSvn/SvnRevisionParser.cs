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

            while (i < str.Length && (str[i] == 'r' || str[i] == ' '))
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

        // Note: in subversion this function called 'svn_opt_parse_revision'
        public static SvnRevisionRange ParseSvnRevisionRange(string str)
        {
            string[] tokens = str.Split(new char[] { ':' });

            if (tokens.Length == 1)
            {
                SvnRevision revision = ParseSvnRevision(tokens[0]);
                return new SvnRevisionRange(revision, revision);
            }
            else if (tokens.Length == 2)
            {
                return new SvnRevisionRange(ParseSvnRevision(tokens[0]), ParseSvnRevision(tokens[1]));
            }
            else
            {
                throw new ArgumentException("Please specify correct revision range.", "Revision");
            }
        }
    }
}
