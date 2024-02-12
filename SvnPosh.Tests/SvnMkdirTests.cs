using NUnit.Framework.Legacy;
using NUnit.Framework;
using SvnPosh.Tests.TestUtils;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;

namespace SvnPosh.Tests
{
    public class SvnMkdirTests
    {
        [Test]
        public void SimpleTest()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = PowerShellUtils.RunScript(
                    $"cd '{sb.WcPath}'",
                    $"svn-mkdir dir_1");

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
