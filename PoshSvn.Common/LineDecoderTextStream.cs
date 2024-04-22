// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Text;

namespace PoshSvn.Common
{
    public class LineDecoderTextStream : ITextStream
    {
        readonly StringBuilder line;
        private readonly ITextLineStream output;

        public LineDecoderTextStream(ITextLineStream output)
        {
            this.output = output;
            line = new StringBuilder();
        }

        public void Dispose()
        {
            if (line.Length > 0)
            {
                output.WriteLine(line.ToString());
            }
        }

        public void Write(char[] chars, int startIndex, int charCount)
        {
            for (int i = startIndex; i < charCount; i++)
            {
                char ch = chars[i];
                if (ch == '\n')
                {
                    output.WriteLine(line.ToString());
                    line.Clear();
                }
                else if (ch == '\r')
                {
                    // Just skip \r for now..
                }
                else
                {
                    line.Append(ch);
                }
            }
        }
    }
}
