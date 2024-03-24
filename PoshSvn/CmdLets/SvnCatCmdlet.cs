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
            else if (Raw)
            {
                TextStream textStream = new TextStream(this);

                return new DecoderStream(textStream, Encoding.UTF8);
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

        private class ByteStream : CatStreamBase
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
