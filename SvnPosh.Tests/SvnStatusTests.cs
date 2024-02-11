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
