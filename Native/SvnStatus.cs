using SharpSvn;
using System;
using System.Management.Automation;

namespace SvnPosh
{
    [Cmdlet("Invoke", "SvnStatus")]
    [OutputType(typeof(SvnStatusOutput))]
    public class SvnStatus : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string Path { get; set; }

        [Parameter()]
        public SwitchParameter All { get; set; }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                client.Status(Path, new SvnStatusArgs()
                    {
                        RetrieveAllEntries = All
                    },
                    new EventHandler<SvnStatusEventArgs>((sender, e) =>
                    {
                        WriteObject(new SvnStatusOutput
                        {
                            Status = e.LocalNodeStatus,
                            Path = e.Path
                        });
                    }));
            }
        }
    }

    public class SvnStatusOutput
    {
        public SharpSvn.SvnStatus Status { get; set; }

        public string Path { get; set; }
    }
}
