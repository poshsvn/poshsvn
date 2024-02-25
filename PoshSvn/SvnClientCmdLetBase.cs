using SharpSvn;

namespace PoshSvn
{
    public abstract class SvnClientCmdletBase : SvnCmdletBase
    {
        protected SvnClient SvnClient;

        public SvnClientCmdletBase()
        {
        }

        protected abstract void Execute();

        protected override void BeginProcessing()
        {
            SvnClient = new SvnClient();
        }

        protected override void ProcessRecord()
        {
            Execute();
        }

        protected override void EndProcessing()
        {
            SvnClient.Dispose();
        }
    }
}
