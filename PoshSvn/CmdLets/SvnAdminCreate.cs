// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;
using SharpSvn;

namespace PoshSvn.CmdLets
{
    [Cmdlet("Invoke", "SvnAdminCreate")]
    [Alias("svnadmin-create")]
    public class SvnAdminCreate : SvnCmdletBase
    {
        [Parameter(Position = 0, ValueFromRemainingArguments = true, Mandatory = true)]
        public string[] Path { get; set; }

        [Parameter()]
        [Alias("fs-type", "type", "fs")]
        public RepositoryType RepositoryType { get; set; } = RepositoryType.FsFs;

        protected override void ProcessRecord()
        {
            using (SvnRepositoryClient client = new SvnRepositoryClient())
            {
                foreach (string path in Path)
                {
                    string resolvedPath = GetPathTarget(path);
                    client.CreateRepository(resolvedPath, new SvnCreateRepositoryArgs
                    {
                        RepositoryType = RepositoryType.ConvertToSvnRepositoryFileSystem(),
                    });

                    WriteVerbose(string.Format("Repository '{0}' created.", resolvedPath));
                }
            }
        }
    }
}
