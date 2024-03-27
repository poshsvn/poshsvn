// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnLockCmdletTests
    {
        [Test]
        public void NotFoundTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("Set-Content wc/tree.jpg abc");
                sb.RunScript("Set-Content wc/house.jpg abc");
                sb.RunScript("svn-add wc/tree.jpg wc/house.jpg");

                Assert.Throws<SharpSvn.SvnWorkingCopyPathNotFoundException>(() =>
                    sb.RunScript("svn-lock wc/tree.jpg wc/house.jpg"));
            }
        }

        [Test]
        public void BasicTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("Set-Content wc/test.txt abc");
                sb.RunScript("svn-add wc/test.txt");
                sb.RunScript("svn-commit wc -m test");
                var actual = sb.RunScript("svn-lock wc/test.txt");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.LockLocked,
                            Path = Path.Combine(sb.WcPath, "test.txt"),
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void LockByUrlTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("Set-Content wc/test.txt abc");
                sb.RunScript("svn-add wc/test.txt");
                sb.RunScript("svn-commit wc -m test");
                var actual = sb.RunScript($"svn-lock {sb.ReposUrl}/test.txt");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.LockLocked,
                            Path = "test.txt",
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void BasicTest2()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("Set-Content wc/test.txt abc");
                sb.RunScript("svn-add wc/test.txt");
                sb.RunScript("svn-commit wc -m test");
                sb.RunScript("svn-lock wc/test.txt");
                sb.RunScript("svn-move wc/test.txt wc/qq.txt");
            }
        }

        [Test]
        public void StatusTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("Set-Content wc/tree.jpg abc");
                sb.RunScript("Set-Content wc/house.jpg abc");
                sb.RunScript("svn-add wc/tree.jpg wc/house.jpg");
                sb.RunScript("svn-commit wc -m test");
                sb.RunScript("Set-Content wc/house.jpg abc,xyz");
                sb.RunScript("svn-lock wc/tree.jpg wc/house.jpg");
                var actual = sb.RunScript("svn-status wc");

                CollectionAssert.AreEqual(
                       new[]
                       {
                            @"",
                            @"Status  Path",
                            @"------  ----",
                            @"M L     wc\house.jpg",
                            @"  L     wc\tree.jpg",
                            @"",
                            @"",
                       },
                       sb.FormatObject(actual, "Format-Table"));
            }
        }
    }
}
