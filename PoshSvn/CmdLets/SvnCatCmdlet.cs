// Copyright (c) Timofei Zhakov. All rights reserved.

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

        [Parameter()]
        public SwitchParameter Raw { get; set; }

        protected override void Execute()
        {
            SvnResolvedTarget resolvedTarget = ResolveTarget(Target);
            SharpSvn.SvnTarget sharpSvnTarget = resolvedTarget.ConvertToSharpSvnTarget();

            using (Stream stream = GetStream())
            {
                SvnClient.Write(sharpSvnTarget, stream);
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
                ITextStream textStream;

                if (Raw)
                {
                    textStream = new TextStream(this);
                }
                else
                {
                    ITextLineStream lineStream = new TextLineStream(this);
                    textStream  = new LineDecoderTextStream(lineStream);
                }

                return new DecoderStream(textStream, Encoding.UTF8);
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

        private class TextStream : ITextStream
        {
            private readonly SvnCatCmdlet owner;
            private readonly StringBuilder sb;

            public TextStream(SvnCatCmdlet owner)
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

        private class ByteStream : WritableStreamBase
        {
            private readonly SvnCatCmdlet owner;

            public ByteStream(SvnCatCmdlet owner)
            {
                this.owner = owner;
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                for (int i = offset; i < buffer.Length; i++)
                {
                    owner.WriteObject(buffer[i]);
                }
            }
        }
    }
}
