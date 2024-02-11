using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
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
            PowerShell ps = PowerShell.Create();

            var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ps.AddCommand("ipmo").AddArgument(Path.Combine(assemblyDirectory, @"..\SvnPosh\SvnPosh.psd1")).Invoke();

            var reposPath = Path.Combine(RootPath, "repos");
            Directory.CreateDirectory(reposPath);
            ps.AddScript($"svnadmin-create '{reposPath}'").Invoke();

            ReposUrl = "file:///" + reposPath.Replace('\\', '/');
            var wcPath = Path.Combine(RootPath, "wc");
            using (var client = new SvnClient())
            {
                client.CheckOut(new SvnUriTarget(ReposUrl), wcPath);
            }

            foreach (var err in ps.Streams.Error)
            {
                Console.Error.WriteLine(err);
            }

            ReposPath = reposPath;
            WcPath = wcPath;
        }
    }
}
