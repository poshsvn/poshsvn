// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;

namespace PoshSvn
{
    public class TextLineStream : ITextLineStream
    {
        private readonly PSCmdlet owner;

        public TextLineStream(PSCmdlet owner)
        {
            this.owner = owner;
        }

        public void WriteLine(string line)
        {
            owner.WriteObject(line);
        }
    }
}
