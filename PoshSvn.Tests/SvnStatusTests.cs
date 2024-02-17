using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnStatusTests
    {
        [Test]
        public void SimpleTest()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript($"(svn-status wc -All | Out-String -stream).TrimEnd()");

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
