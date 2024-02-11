using NUnit.Framework;
using NUnit.Framework.Legacy;
using SvnPosh.Tests.TestUtils;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;

namespace SvnPosh.Tests
{
    public class SvnStatusTests
    {
        [Test]
        public void SimpleTest()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = PowerShellUtils.RunScript($"(svn-status '{sb.WcPath}' -All | Out-String -stream).TrimEnd()");

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
