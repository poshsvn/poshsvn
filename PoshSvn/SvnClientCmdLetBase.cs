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
            SvnClient.Notify += NotifyEventHandler;
            SvnClient.Progress += ProgressEventHandler;
            SvnClient.Committing += CommittingEventHandler;
            SvnClient.Committed += CommittedEventHandler;
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
