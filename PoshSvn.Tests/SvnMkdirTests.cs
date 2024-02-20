using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnMkdirTests
    {
        [Test]
        public void CreateDirectoryInWorkingCopy()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript($"cd wc; (svn-mkdir dir_1 | Out-String -stream).TrimEnd()");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        $@"",
                        $@"Action  Path",
                        $@"------  ----",
                        $@"A       {sb.WcPath}\dir_1",
                        $@"",
                        $@"",
                    },
                    Array.ConvertAll(actual.ToArray(), a => (string)a.BaseObject));

                actual = sb.RunScript($"(svn-status wc | Out-String -stream).TrimEnd()");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        $@"",
                        $@"Status  Path",
                        $@"------  ----",
                        $@"A       wc\dir_1",
                        $@"",
                        $@"",
                    },
                    Array.ConvertAll(actual.ToArray(), a => (string)a.BaseObject));

                actual = sb.RunScript($"cd wc; svn-commit -m test");
            }
        }
    }
}
