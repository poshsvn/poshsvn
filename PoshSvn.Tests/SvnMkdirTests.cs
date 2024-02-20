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

namespace PoshSvn.Tests
{
    public class SvnMkdirTests
    {
        [Test]
        public void BasicTest()
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

        [Test]
        public void LocalMkdirOutputTest()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.RunScript($@"svn-mkdir wc\test");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnMkdirOutput
                        {
                            Path = Path.Combine(sb.WcPath, "test")
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void MkdirWithParents()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.RunScript($@"svn-mkdir wc\a\b\c -parent");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnMkdirOutput
                        {
                            Action = SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, @"a")
                        },
                        new SvnMkdirOutput
                        {
                            Action = SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, @"a\b")
                        },
                        new SvnMkdirOutput
                        {
                            Action = SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, @"a\b\c")
                        },
                    },
                    actual);

                Assert.Throws<SvnSystemException>(() => sb.RunScript($@"svn-mkdir wc\a\b\c"));
            }
        }

        [Test]
        public void RemoteMkdirTest()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.RunScript($"svn-mkdir -url '{sb.ReposUrl}/test' -m 'test'");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnCommitOutput
                        {
                            Revision = 1
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void RemoteMkdirFormatTest()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.RunScript($"(svn-mkdir -url '{sb.ReposUrl}/test' -m 'test' | Out-String -stream).TrimEnd()");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        $@"",
                        $@"Committed revision 1.",
                        $@"",
                        $@"",
                    },
                    Array.ConvertAll(actual.ToArray(), a => (string)a.BaseObject));
            }
        }
    }
}
