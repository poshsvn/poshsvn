// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public class SvnRevisionChange : SvnRevisionRangeBase
    {
        public SvnRevisionChange(string str)
        {
            int i = 0;

            // Check for a leading minus to allow "-c -r42".
            // The is_negative flag is used to handle "-c -42" and "-c -r42".
            // The "-c r-42" case is handled by strtol() returning a
            // negative number.
            bool isNegative = str[i] == '-';
            if (isNegative)
            {
                i++;
            }

            // Allow any number of 'r's to prefix a revision number.
            while (i < str.Length && (str[i] == 'r' || str[i] == ' '))
            {
                i++;
            }

            if (long.TryParse(str.Substring(i), out long revisionNumber))
            {
                if (revisionNumber < 0)
                {
                    throw new ArgumentException("Invalid revision: The revision number cannot be negative here.");
                }

                if (isNegative)
                {
                    StartRevision = new SvnRevision(revisionNumber);
                    EndRevision = new SvnRevision(revisionNumber - 1);
                }
                else
                {
                    StartRevision = new SvnRevision(revisionNumber - 1);
                    EndRevision = new SvnRevision(revisionNumber);
                }
            }
            else
            {
                throw new ArgumentException($"Non-numeric change argument ({str}) given to -c");
            }
        }
    }
}
