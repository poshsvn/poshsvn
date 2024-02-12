using NUnit.Framework.Legacy;
using NUnit.Framework;
using SvnPosh.Tests.TestUtils;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace SvnPosh.Tests
{
    public class SvnMkdirTests
    {
        [Test]
        public void CreateDirectoryInWorkingCopy()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = PowerShellUtils.RunScript(
                    $"cd '{sb.WcPath}'",
                    $"(svn-mkdir dir_1 | Out-String -stream).TrimEnd()");

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

                actual = PowerShellUtils.RunScript(
                    $"(svn-status '{sb.WcPath}' | Out-String -stream).TrimEnd()");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        $@"",
                        $@"Status  Path",
                        $@"------  ----",
                        $@"A       {sb.WcPath}\dir_1",
                        $@"",
                        $@"",
                    },
                    Array.ConvertAll(actual.ToArray(), a => (string)a.BaseObject));

                actual = PowerShellUtils.RunScript(
                    $"cd '{sb.WcPath}'",
                    $"svn-commit -m test");
            }
        }
    }
}
