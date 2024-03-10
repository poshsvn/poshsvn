// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using PoshSvn.CmdLets;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnCopyTests
    {
        [Test]
        public void BasicOutputTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content 'wc\a.txt' 'test'");
                sb.RunScript(@"svn-add 'wc\a.txt'");
                var actual = sb.RunScript(@"svn-copy 'wc\a.txt' 'wc\b.txt'");

                PSObjectAssert.AreEqual(
                   new SvnNotifyOutput[]
                   {
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, "b.txt")
                        },
                   },
                   actual);
            }
        }

        [Test]
        public void BasicStatusTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content 'wc\a.txt' 'test'");
                sb.RunScript(@"svn-add 'wc\a.txt'");
                sb.RunScript(@"svn-copy 'wc\a.txt' 'wc\b.txt'");

                var actual = sb.RunScript(@"svn-status wc");

                PSObjectAssert.AreEqual(
                   new SvnLocalStatusOutput[]
                   {
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.Added,
                            Path = Path.Combine(sb.WcPath, "a.txt"),
                            LocalTextStatus = SharpSvn.SvnStatus.Modified,
                            Versioned = true,
                            Conflicted = false,
                            LocalCopied = false,
                        },
                       new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.Added,
                            Path = Path.Combine(sb.WcPath, "b.txt"),
                            LocalTextStatus = SharpSvn.SvnStatus.Modified,
                            Versioned = true,
                            Conflicted = false,
                            LocalCopied = false,
                        },
                   },
                   actual);
            }
        }

        [Test]
        public void ManySourcesTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content 'wc\a.txt' 'test'; svn-add 'wc\a.txt'");
                sb.RunScript(@"Set-Content 'wc\b.txt' 'test'; svn-add 'wc\b.txt'");
                sb.RunScript(@"Set-Content 'wc\c.txt' 'test'; svn-add 'wc\c.txt'");
                sb.RunScript(@"svn-mkdir wc\x");

                var actual = sb.RunScript(@"svn-copy wc\a.txt, wc\b.txt, wc\c.txt 'wc\x'");

                PSObjectAssert.AreEqual(
                   new SvnNotifyOutput[]
                   {
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x\a.txt") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x\b.txt") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x\c.txt") },
                   },
                   actual);
            }
        }

        [Test]
        public void CreateParentsTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content 'wc\a.txt' 'test'; svn-add 'wc\a.txt'");
                sb.RunScript(@"Set-Content 'wc\b.txt' 'test'; svn-add 'wc\b.txt'");
                sb.RunScript(@"Set-Content 'wc\c.txt' 'test'; svn-add 'wc\c.txt'");

                var actual = sb.RunScript(@"svn-copy wc\a.txt, wc\b.txt, wc\c.txt 'wc\x\y\z' -Parents");

                PSObjectAssert.AreEqual(
                   new SvnNotifyOutput[]
                   {
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x\y") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x\y\z") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x\y\z\a.txt") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x\y\z\b.txt") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x\y\z\c.txt") },
                   },
                   actual);
            }
        }

        [Test]
        public void FromRemoteTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content 'wc\a.txt' 'test'");
                sb.RunScript(@"svn-add 'wc\a.txt'");
                sb.RunScript(@"svn-commit wc -m 'test'");

                var actual = sb.RunScript($@"svn-copy '{sb.ReposUrl}/a.txt' 'wc\b.txt'");

                PSObjectAssert.AreEqual(
                   new SvnNotifyOutput[]
                   {
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, "b.txt")
                        },
                   },
                   actual);
            }
        }

        [Test]
        public void FromRemoteManySourcesTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content 'wc\a.txt' 'test'; svn-add 'wc\a.txt'");
                sb.RunScript(@"Set-Content 'wc\b.txt' 'test'; svn-add 'wc\b.txt'");
                sb.RunScript(@"Set-Content 'wc\c.txt' 'test'; svn-add 'wc\c.txt'");
                sb.RunScript(@"svn-commit wc -m 'test'");

                var actual = sb.RunScript(@"svn-copy wc\a.txt, wc\b.txt, wc\c.txt 'wc\x' -Parents");

                PSObjectAssert.AreEqual(
                   new SvnNotifyOutput[]
                   {
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x\a.txt") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x\b.txt") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.Add, Path = Path.Combine(sb.WcPath, @"x\c.txt") },
                   },
                   actual);
            }
        }

        [Test]
        public void FromLocalToRemoteTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content 'wc\a.txt' 'test'");
                sb.RunScript(@"svn-add 'wc\a.txt'");

                var actual = sb.RunScript($@"svn-copy 'wc\a.txt' '{sb.ReposUrl}\b.txt' -m 'test'");

                PSObjectAssert.AreEqual(
                   new object[]
                   {
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.CommitAdded,
                            Path = Path.Combine(sb.WcPath, "a.txt")
                        },
                        new SvnCommitOutput
                        {
                            Revision = 1
                        },
                   },
                   actual);

                // TODO: maybe test by list ?
            }
        }

        [Test]
        public void FromLocalToRemoteWithManySourcesTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content 'wc\a.txt' 'test'; svn-add 'wc\a.txt'");
                sb.RunScript(@"Set-Content 'wc\b.txt' 'test'; svn-add 'wc\b.txt'");
                sb.RunScript(@"Set-Content 'wc\c.txt' 'test'; svn-add 'wc\c.txt'");

                var actual = sb.RunScript($@"svn-copy wc\* '{sb.ReposUrl}\x\y\z' -Parents -m 'test'");

                PSObjectAssert.AreEqual(
                   new object[]
                   {
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.CommitAdded, Path = Path.Combine(sb.WcPath, @"a.txt") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.CommitAdded, Path = Path.Combine(sb.WcPath, @"b.txt") },
                        new SvnNotifyOutput { Action = SharpSvn.SvnNotifyAction.CommitAdded, Path = Path.Combine(sb.WcPath, @"c.txt") },
                        new SvnCommitOutput
                        {
                            Revision = 1
                        },
                   },
                   actual);
            }
        }

        [Test]
        public void RemoteCopyTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content 'wc\a.txt' 'test'");
                sb.RunScript(@"svn-add 'wc\a.txt'");
                sb.RunScript(@"svn-commit wc -m 'test'");

                var actual = sb.RunScript($@"svn-copy '{sb.ReposUrl}\a.txt' '{sb.ReposUrl}\b.txt' -m 'test'");

                PSObjectAssert.AreEqual(
                   new object[]
                   {
                        new SvnCommitOutput
                        {
                            Revision = 2
                        },
                   },
                   actual);
            }
        }

        [Test]
        public void RemoteCopyDirectoryTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($@"mkdir 'wc\trunk'");
                sb.RunScript($@"Set-Content 'wc\trunk\a.txt' 'test'");
                sb.RunScript($@"Set-Content 'wc\trunk\b.txt' 'test'");
                sb.RunScript($@"Set-Content 'wc\trunk\c.txt' 'test'");
                sb.RunScript($@"svn-add 'wc\trunk' -Depth Infinity");
                sb.RunScript($@"svn-commit wc -m 'test'");
                sb.RunScript($@"svn-mkdir '{sb.ReposUrl}/branches' -m 'create branches directory'");

                var actual = sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/the-feature' -m 'create new feature branch'");

                PSObjectAssert.AreEqual(
                   new object[]
                   {
                        new SvnCommitOutput
                        {
                            Revision = 3
                        },
                   },
                   actual);

                // TODO: maybe test by list ?
            }
        }
    }
}
