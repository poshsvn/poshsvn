// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnTargetTests
    {
        [Test]
        public void ParseFromAbsolutePath()
        {
            using (var sb = new PowerShellSandbox())
            {
                PSObjectAssert.AreEqual(
                    new[]
                    {
                        SvnTarget.FromPath(sb.RootPath)
                    },
                    sb.RunScript($@"New-SvnTarget '{sb.RootPath}'"));
            }
        }

        [Test]
        public void WithCopyTest1()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content 'wc\a.txt' 'test'");
                sb.RunScript(@"svn-add 'wc\a.txt'");
                var actual = sb.RunScript(@"svn-copy (New-SvnTarget wc\a.txt) (New-SvnTarget -LiteralPath wc\b.txt)");

                PSObjectAssert.AreEqual(
                   new[]
                   {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, "b.txt")
                        },
                   },
                   actual);
            }
        }

        [Test]
        public void ParseFromAbsoluteUrl()
        {
            using (var sb = new PowerShellSandbox())
            {
                PSObjectAssert.AreEqual(
                    new[]
                    {
                        SvnTarget.FromUrl("http://svn.example.com/repos/test/foo.c")
                    },
                    sb.RunScript($@"New-SvnTarget 'http://svn.example.com/repos/test/foo.c'"));
            }
        }

        [Test]
        public void DeleteFromGetChildItemsPipeline1()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("svn-mkdir wc/a wc/b wc/c");
                sb.RunScript("'qq' > wc/a.txt; svn-add wc/a.txt; svn-commit wc/a.txt -m test");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "a") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "b") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "c") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "a.txt") },
                    },
                    sb.RunScript("ls wc | % { svn-delete $_ }"));
            }
        }

        [Test]
        public void MegaPipeline()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("svn-mkdir wc/a wc/b wc/c");
                sb.RunScript("'qq' > wc/a.txt; svn-add wc/a.txt; svn-commit wc/a.txt -m test");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "a") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "b") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "c") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "a.txt") },
                    },
                    sb.RunScript("ls wc | New-SvnTarget | svn-delete"));
            }
        }

        [Test]
        [Ignore("I don't know how to do this...")]
        public void DeleteFromGetChildItemsPipeline2()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("svn-mkdir wc/a wc/b wc/c");
                sb.RunScript("'qq' > wc/a.txt; svn-add wc/a.txt; svn-commit wc/a.txt -m test");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "a") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "b") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "c") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.Delete, Path = Path.Combine(sb.WcPath, "a.txt") },
                    },
                    sb.RunScript("ls wc | svn-delete"));
            }
        }

        [Test]
        public void NewSvnTargetFromGetChildItemsPipeline()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("svn-mkdir wc/a wc/b wc/c");
                sb.RunScript("'qq' > wc/a.txt; svn-add wc/a.txt; svn-commit wc/a.txt -m test");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        SvnTarget.FromLiteralPath(Path.Combine(sb.WcPath, "a")),
                        SvnTarget.FromLiteralPath(Path.Combine(sb.WcPath, "b")),
                        SvnTarget.FromLiteralPath(Path.Combine(sb.WcPath, "c")),
                        SvnTarget.FromLiteralPath(Path.Combine(sb.WcPath, "a.txt")),
                    },
                    sb.RunScript("ls wc | New-SvnTarget"));
            }
        }

        [Test]
        public void ParsePegRevisionTargetTest1()
        {
            SvnTarget.ParsePegRevisionTarget(@"C:\path\to\file", out var remainingTarget, out var revision);
            ClassicAssert.AreEqual(@"C:\path\to\file", remainingTarget);
            ClassicAssert.AreEqual(null, revision);
        }

        [Test]
        public void ParsePegRevisionTargetTest2()
        {
            SvnTarget.ParsePegRevisionTarget(@"C:\path\to\file@123", out var remainingTarget, out var revision);
            ClassicAssert.AreEqual(@"C:\path\to\file", remainingTarget);
            ClassicAssert.AreEqual(new SvnRevision("123"), revision);
        }

        [Test]
        public void ParsePegRevisionTargetTest3()
        {
            SvnTarget.ParsePegRevisionTarget(@"http://svn.example.com/svn/repo/trunk/test.txt@HEAD", out var remainingTarget, out var revision);
            ClassicAssert.AreEqual(@"http://svn.example.com/svn/repo/trunk/test.txt", remainingTarget);
            ClassicAssert.AreEqual(new SvnRevision("HEAD"), revision);
        }
    }
}
