using System.IO;
using SharpSvn;

namespace SvnPosh.Tests.TestUtils
{
    public class WcSandbox : Sandbox
    {
        public string ReposPath { get; }
        public string ReposUrl { get; }
        public string WcPath { get; }

        public WcSandbox()
        {
            var reposPath = Path.Combine(RootPath, "repos");
            using (var client = new SvnRepositoryClient())
            {
                client.CreateRepository(reposPath);
            }

            ReposUrl = "file:///" + reposPath.Replace('\\', '/');
            var wcPath = Path.Combine(RootPath, "wc");
            using (var client = new SvnClient())
            {
                client.CheckOut(new SvnUriTarget(ReposUrl), wcPath);
            }

            ReposPath = reposPath;
            WcPath = wcPath;
        }
    }
}
