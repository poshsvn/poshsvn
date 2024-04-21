// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Text;

namespace PoshSvn.Common
{
    public class DecoderStream : WritableStreamBase
    {
        private readonly ITextStream output;
        private readonly Decoder decoder;

        public DecoderStream(ITextStream output,
                             Encoding encoding)
        {
            this.output = output;
            decoder = encoding.GetDecoder();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Process(buffer, offset, count, false);
        }

        public override void Close()
        {
            base.Close();
            Process(Array.Empty<byte>(), 0, 0, true);
            output.Dispose();
        }

        private void Process(byte[] buffer, int offset, int count, bool flush)
        {
            int charsCount = decoder.GetCharCount(buffer, offset, count, flush);

            char[] chars = new char[charsCount];

            charsCount = decoder.GetChars(buffer, offset, count, chars, 0, flush);

            if (charsCount > 0)
            {
                output.Write(chars, 0, charsCount);
            }
        }
    }
}
