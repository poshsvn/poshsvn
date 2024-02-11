using SharpSvn;
using System.Management.Automation;

namespace SvnPosh
{
    [Cmdlet("Invoke", "SvnAdminCreate")]
    [Alias("svnadmin-create", "svn-admin-create")]
    public class SvnAdminCreate : SvnCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string[] Path { get; set; }

        [Parameter()]
        [Alias("fs-type", "type", "fs")]
        public RepositoryType RepositoryType { get; set; } = RepositoryType.FsFs;

        protected override void ProcessRecord()
        {
            using (SvnRepositoryClient client = new SvnRepositoryClient())
            {
                string[] resolvedPaths = GetPathTargets(Path, null);

                foreach (string resolvedPath in resolvedPaths)
                {
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
