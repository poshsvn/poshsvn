// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;

namespace PoshSvn
{
    public class ByteStream : WritableStreamBase
    {
        private readonly PSCmdlet owner;

        public ByteStream(PSCmdlet owner)
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
