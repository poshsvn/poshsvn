using SharpSvn;
using System;
using System.Management.Automation;

namespace SvnPosh
{
    [Cmdlet("Invoke", "SvnMkdir")]
    [Alias("svn-mkdir")]
    [OutputType(typeof(SvnMkdirOutput))]
    public class SvnMkDir : SvnCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Path")]
        public string[] Path { get; set; }

        [Parameter()]
        public SwitchParameter Parents { get; set; }

        protected override void ProcessRecord()
        {
            using (SvnClient client = new SvnClient())
            {
                SvnCreateDirectoryArgs args = new SvnCreateDirectoryArgs
                {
                    CreateParents = Parents,
                };

                args.Notify += new EventHandler<SvnNotifyEventArgs>((_, e) =>
                {
                    WriteObject(new SvnMkdirOutput
                    {
                        Action = e.Action,
                        Path = e.Path
                    });
                });

                string[] resolvedPaths = GetPathTargets(null, Path);

                client.CreateDirectories(resolvedPaths, args);
            }
        }
    }

    public class SvnMkdirOutput
    {
        public SvnNotifyAction Action { get; set; }
        public string ActionString => SvnUtils.GetActionStringShort(Action);
        public string Path { get; set; }
    }
}
