using System.IO;

namespace PoshSvn.Tests.TestUtils
{
    public class WcSandbox : Sandbox
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

            PowerShellUtils.RunScript($"svnadmin-create '{ReposPath}'",
                                      $"svn-checkout '{ReposUrl}' '{WcPath}'");
        }
    }
}
