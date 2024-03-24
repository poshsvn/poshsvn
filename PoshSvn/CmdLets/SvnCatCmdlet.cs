// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.IO;
using System.Management.Automation;
using System.Text;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnCat")]
    [Alias("svn-cat")]
    [OutputType(typeof(string), typeof(byte))]
    public class SvnCatCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0)]
        public SvnTarget Target { get; set; }

        [Parameter()]
        public SwitchParameter AsByteStream { get; set; }

        protected override void Execute()
        {
            SharpSvn.SvnTarget target = TargetCollection.ConvertTargetToSvnTarget(GetTarget(Target));

            using (Stream stream = GetStream())
            {
                SvnClient.Write(target, stream);
            }
        }

        protected Stream GetStream()
        {
            if (AsByteStream)
            {
                return new ByteStream(this);
            }
            else
            {
                TextLineStream textStream = new TextLineStream(this);
                LineDecoderTextStream lineStream = new LineDecoderTextStream(textStream);

                return new DecoderStream(lineStream, Encoding.UTF8);
            }
        }

        private class TextLineStream : ITextLineStream
        {
            private readonly SvnCatCmdlet owner;

            public TextLineStream(SvnCatCmdlet owner)
            {
                this.owner = owner;
            }

            public void WriteLine(string line)
            {
                owner.WriteObject(line);
            }
        }

        private class ByteStream : Stream
        {
            private readonly SvnCatCmdlet owner;

            public ByteStream(SvnCatCmdlet owner)
            {
                this.owner = owner;
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
                for (int i = offset; i < buffer.Length; i++)
                {
                    byte b = buffer[i];
                    owner.WriteObject(b);
                }
            }
        }
    }
}
