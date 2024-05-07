// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using System.Text;

namespace PoshSvn
{
    public class TextStream : ITextStream
    {
        private readonly PSCmdlet owner;
        private readonly StringBuilder sb;

        public TextStream(PSCmdlet owner)
        {
            this.owner = owner;
            sb = new StringBuilder();
        }

        public void Dispose()
        {
            owner.WriteObject(sb.ToString());
        }

        public void Write(char[] chars, int startIndex, int charCount)
        {
            sb.Append(chars, startIndex, charCount);
        }
    }
}
