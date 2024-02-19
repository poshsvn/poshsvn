using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;
using SharpSvn;

namespace PoshSvn.Tests
{
    public class SvnDeleteTests
    {
        [Test]
        public void BasicTest()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript(
                    $"cd wc",
                    $"svn-mkdir dir",
                    $"svn-delete dir",
                    $"svn-status");

                PSObjectAssert.AreEqual(
                    new SvnLocalStatusOutput[]
                    {
                    },
                    actual);
            }
        }

        [Test]
        public void OutputTest()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript(
                    $"cd wc",
                    $"svn-mkdir dir",
                    $"svn-delete dir");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnDeleteOutput
                        {
                            Path = Path.Combine(sb.WcPath, "dir"),
                            Action = SvnNotifyAction.Delete,
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void FormattedOutputTest()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript(
                    $"cd wc",
                    $"svn-mkdir dir",
                    $"(svn-delete dir | Out-String -Stream).TrimEnd()");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        $@"",
                        $@"Action  Path",
                        $@"------  ----",
                        $@"D       dir",
                        "",
                        "",
                    },
                    Array.ConvertAll(actual.ToArray(), a => a.BaseObject));
            }
        }

        [Test]
        public void IncorrectParametersTests()
        {
            using (var sb = new WcSandbox())
            {
                Assert.Throws<ItemNotFoundException>(() => sb.RunScript(
                    @"svn-delete wc\dir"));

                Assert.Throws<SvnInvalidNodeKindException>(() => sb.RunScript(
                    @"mkdir non_wc\dir",
                    @"svn-delete non_wc\dir"));
            }
        }
    }
}
