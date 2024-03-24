// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.IO;
using System.Text;

namespace PoshSvn
{
    public interface ITextStream : IDisposable
    {
        void Write(char[] chars, int startIndex, int charCount);
    }

    public class DecoderStream : Stream
    {
        private readonly ITextStream output;
        private readonly Decoder decoder;

        public DecoderStream(ITextStream output, 
                             Encoding encoding)
        {
            this.output = output;
            decoder = encoding.GetDecoder();
        }

        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => true;

        public override long Length => throw new NotSupportedException();

        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
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
