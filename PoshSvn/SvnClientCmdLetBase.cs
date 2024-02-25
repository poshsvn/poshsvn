using SharpSvn;

namespace PoshSvn
{
    public abstract class SvnClientCmdletBase : SvnCmdletBase
    {
        protected SvnClient client;

        public SvnClientCmdletBase()
        {
        }

        protected abstract void Execute();

        protected override void BeginProcessing()
        {
            client = new SvnClient();
        }

        protected override void ProcessRecord()
        {
            Execute();
        }

        protected override void EndProcessing()
        {
            client.Dispose();
        }
    }
}
