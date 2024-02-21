using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnMove")]
    [Alias("svn-move", "Move-SvnItem")]
    [OutputType(typeof(SvnNotifyOutput), typeof(SvnCommitOutput))]
    public class SvnMove : SvnCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string[] Source { get; set; }

        [Parameter(Position = 1, Mandatory = true)]
        public string Destination { get; set; }

        [Parameter()]
        public SwitchParameter Force { get; set; }

        [Parameter()]
        public SwitchParameter Parents { get; set; }

        [Parameter()]
        public SwitchParameter AllowMixedRevisions { get; set; }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                SvnMoveArgs args = new SvnMoveArgs
                {
                    Force = Force,
                    CreateParents = Parents,
                    AllowMixedRevisions = AllowMixedRevisions,
                };

                args.Progress += Progress;
                args.Notify += Notify;

                client.Move(GetPathTargets(Source, null), GetPathTarget(Destination), args);
            }
        }

        protected override object GetNotifyOutput(SvnNotifyEventArgs e)
        {
            return new SvnNotifyOutput
            {
                Action = e.Action,
                Path = e.Path,
            };
        }
    }
}
