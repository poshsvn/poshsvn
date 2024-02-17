using System.IO;

namespace PoshSvn.Tests.TestUtils
{
    public class WcSandbox : PowerShellSandbox
    {
        public string ReposPath { get; }
        public string ReposUrl { get; }
        public string WcPath { get; }

        public WcSandbox()
        {
            ReposPath = Path.Combine(RootPath, "repos");
            ReposUrl = "file:///" + ReposPath.Replace('\\', '/');
            WcPath = Path.Combine(RootPath, "wc");

            Directory.CreateDirectory(ReposPath);

            RunScript($@"svnadmin-create .\repos",
                      $@"svn-checkout '{ReposUrl}' .\wc");
        }
    }
}
