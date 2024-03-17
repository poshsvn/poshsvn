// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class NewSvnTargetTests
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
                            Action = SharpSvn.SvnNotifyAction.Add,
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
    }
}
