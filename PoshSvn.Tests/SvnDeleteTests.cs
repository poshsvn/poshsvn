// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.CmdLets;
using PoshSvn.Tests.TestUtils;
using SharpSvn;
using DriveNotFoundException = System.Management.Automation.DriveNotFoundException;

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
        public void ThrowIfHasAnyOperationalRevisions()
        {
            using (var sb = new WcSandbox())
            {
                Assert.Throws<ArgumentException>(() =>
                    sb.RunScript($"svn-delete http://svn.example.com/repos/test/foo.c@123"));
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
                        new SvnNotifyOutput
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
                var actual = sb.RunScript(
                    $"cd wc",
                    $"svn-mkdir dir",
                    $"(svn-delete dir | Out-String -Stream).TrimEnd()");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        $@"",
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
                Assert.Throws<SharpSvn.SvnUnversionedNodeException>(() => sb.RunScript(
                    @"svn-delete wc\dir"));

                Assert.Throws<SvnInvalidNodeKindException>(() => sb.RunScript(
                    @"mkdir non_wc\dir",
                    @"svn-delete non_wc\dir"));
            }
        }

        [Test]
        public void SvnDeleteRemoteTest()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript(
                    $"svn-mkdir '{sb.ReposUrl}/dir' -m 'add'",
                    $"svn-delete '{sb.ReposUrl}/dir' -m 'delete'");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnCommitOutput
                        {
                            Revision = 2,
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void SvnDeleteRemoteFormatTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($"svn-mkdir (New-SvnTarget -url '{sb.ReposUrl}/dir') -m 'add'");

                var actual = sb.RunScript($"(svn-delete (New-SvnTarget -url '{sb.ReposUrl}/dir') -m 'delete'| Out-String -Stream).TrimEnd()");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        $@"",
                        $@"Committed revision 2.",
                        $@"",
                        $@"",
                    },
                    sb.FormatObject(actual, "Format-Custom"));
            }
        }

        [Test]
        public void IncorrectParameters()
        {
            using (var sb = new WcSandbox())
            {
                Assert.Throws<ArgumentException>(() => sb.RunScript(
                    $"svn-delete (New-SvnTarget -url not_uri) -m 'delete'"));

                Assert.Throws<DriveNotFoundException>(() => sb.RunScript(
                    $"svn-delete (New-SvnTarget -path http://example.com)"));

                Assert.Throws<ArgumentException>(() => sb.RunScript(
                    $"svn-delete wc http://example.com"));
            }
        }

        [Test]
        public void DeleteNotExsistingItem()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($"svn-mkdir wc/a");
                sb.RunScript($"svn-commit wc -m test");
                sb.RunScript($"rm wc/a");
                sb.RunScript($"svn-delete wc/a");
            }
        }

        [Test]
        public void DeleteNotExsistingItemViaLiteralPath()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($"svn-mkdir wc/a");
                sb.RunScript($"svn-commit wc -m test");
                sb.RunScript($"rm wc/a");
                sb.RunScript($"svn-delete (New-SvnTarget -LiteralPath wc/a)");
            }
        }
    }
}
