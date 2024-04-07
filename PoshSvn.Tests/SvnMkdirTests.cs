// Copyright (c) Timofei Zhakov. All rights reserved.

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
                        $@"A       dir_1",
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
                        new SvnNotifyOutput
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
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, @"a")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, @"a\b")
                        },
                        new SvnNotifyOutput
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
        public void DirectoryAlreadyExists()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($@"svn-mkdir wc\test");
                Assert.Throws<SvnSystemException>(() => sb.RunScript($@"svn-mkdir wc\test"));
            }
        }

        [Test]
        public void RemoteMkdirTest()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.RunScript($"svn-mkdir '{sb.ReposUrl}/test' -m 'test'");

                PSObjectAssert.AreEqual(
                    new object[]
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
                var actual = sb.RunScript($"(svn-mkdir '{sb.ReposUrl}/test' -m 'test' | Out-String -stream).TrimEnd()");

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

        [Test]
        public void CreateManyDirectoriesViaArrayTest()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript(
                    "cd wc",
                    "$arr = @('a', 'b', 'c')",
                    "$out = svn-mkdir $arr",
                    "($out| Out-String -stream).TrimEnd()");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        $@"",
                        $@"A       a",
                        $@"A       b",
                        $@"A       c",
                        $@"",
                        $@"",
                    },
                    Array.ConvertAll(actual.ToArray(), a => (string)a.BaseObject));
            }
        }

        [Test]
        public void CreateManyDirectoriesViaForLoopTest()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript(
                    "cd wc",
                    "0..3 | svn-mkdir");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, "0")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, "1")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, "2")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, "3")
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void CreateManyDirectoriesViaValueFromRemainingArgumentsTest()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript(
                    "$out = svn-mkdir wc/a wc/b wc/c",
                    "($out| Out-String -stream).TrimEnd()");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        $@"",
                        $@"A       wc\a",
                        $@"A       wc\b",
                        $@"A       wc\c",
                        $@"",
                        $@"",
                    },
                    Array.ConvertAll(actual.ToArray(), a => (string)a.BaseObject));
            }
        }
    }
}
