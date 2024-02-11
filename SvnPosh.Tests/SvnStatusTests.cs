using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SvnPosh.Tests.TestUtils;

namespace SvnPosh.Tests
{
    public class SvnStatusTests
    {
        [Test]
        public void SimpleTest()
        {
            PowerShell ps = PowerShell.Create();

            var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ps.AddCommand("ipmo").AddArgument(Path.Combine(assemblyDirectory, @"..\SvnPosh\SvnPosh.psd1")).Invoke();

            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = ps.AddScript($"(svn-status '{sb.WcPath}' -All | Out-String -stream).TrimEnd()").Invoke();

                foreach (var o in ps.Streams.Warning)
                {
                    Console.WriteLine(o);
                }

                foreach (var o in ps.Streams.Error)
                {
                    Console.Error.WriteLine(o);
                }

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        $@"",
                        $@"Status  Path",
                        $@"------  ----",
                        $@"        {sb.WcPath}",
                        $@"",
                        $@"",
                    },
                    Array.ConvertAll(actual.ToArray(), a => (string)a.BaseObject));
            }
        }
    }
}
