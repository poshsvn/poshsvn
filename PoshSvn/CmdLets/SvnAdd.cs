using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnAdd")]
    [Alias("svn-add", "Add-SvnItem")]
    [OutputType(typeof(SvnNotifyOutput))]
    public class SvnAdd : SvnCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ValueFromRemainingArguments = true)]
        public string[] Path { get; set; }

        [Parameter()]
        public SvnDepth Depth { get; set; } = SvnDepth.Infinity;

        [Parameter()]
        public SwitchParameter Force { get; set; }

        [Parameter()]
        [Alias("no-ignore")]
        public SwitchParameter NoIgnore { get; set; }

        [Parameter()]
        [Alias("no-auto-props")]
        public SwitchParameter NoAutoProps { get; set; }

        [Parameter()]
        public SwitchParameter Parents { get; set; }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                SvnAddArgs args = new SvnAddArgs
                {
                    Depth = Depth.ConvertToSharpSvnDepth(),
                    Force = Force,
                    NoIgnore = NoIgnore,
                    NoAutoProps = NoAutoProps,
                    AddParents = Parents,
                };

                args.Progress += ProgressEventHandler;
                args.Notify += NotifyEventHandler;

                foreach (string path in GetPathTargets(Path, null))
                {
                    client.Add(path, args);
                }
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
