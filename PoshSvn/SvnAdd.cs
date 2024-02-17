using System.Management.Automation;
using System.Management.Automation.Language;
using SharpSvn;

namespace PoshSvn
{
    [Cmdlet("Invoke", "SvnAdd")]
    [Alias("svn-add", "Add-SvnItem")]
    [OutputType(typeof(SvnCommitOutput))]
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

                args.Progress += Progress;
                args.Notify += Notify;

                foreach (string path in GetPathTargets(Path, null))
                {
                    client.Add(path, args);
                }
            }
        }

        protected override object GetNotifyOutput(SvnNotifyEventArgs e)
        {
            return new SvnAddOutput
            {
                Action = e.Action,
                Path = e.Path,
            };
        }
    }

    public class SvnAddOutput
    {
        public SvnNotifyAction Action { get; set; }
        public string ActionString => SvnUtils.GetActionStringShort(Action);
        public string Path { get; set; }
    }
}
