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

        [Parameter()]
        [ArgumentCompleter(typeof(EncodingArgumentCompletions))]
        [EncodingArgumentTransformation()]
        public Encoding Encoding { get; set; } = Encoding.UTF8;

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

                return new DecoderStream(textStream, Encoding);
            }
        }
    }
}
