// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using System.Management.Automation;
using System.Text;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnCat")]
    [Alias("svn-cat")]
    [OutputType(typeof(string))]
    public class SvnCatCmdlet : SvnClientCmdletBase
    {
        [Parameter(Position = 0)]
        public SvnTarget Target { get; set; }

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
            TextLineStream textStream = new TextLineStream(this);
            LineDecoderTextStream lineStream = new LineDecoderTextStream(textStream);

            return new DecoderStream(lineStream, Encoding.UTF8);
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
    }
}
