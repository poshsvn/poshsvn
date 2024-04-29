// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnUnlockCmdletTests
    {
        [Test]
        public void BasicTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("Set-Content wc/test.txt abc");
                sb.RunScript("svn-add wc/test.txt");
                sb.RunScript("svn-commit wc -m test");
                sb.RunScript("svn-lock wc/test.txt");
                var actual = sb.RunScript("svn-unlock wc/test.txt");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.LockUnlocked,
                            Path = Path.Combine(sb.WcPath, "test.txt"),
                        },
                    },
                    actual);
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
                sb.RunScript("svn-unlock wc/tree.jpg wc/house.jpg");
                var actual = sb.RunScript("svn-status wc -All");

                CollectionAssert.AreEqual(
                       new[]
                       {
                            @"",
                            @"Status  Path",
                            @"------  ----",
                            @"        wc",
                            @"M       wc\house.jpg",
                            @"        wc\tree.jpg",
                            @"",
                            @"",
                       },
                       sb.FormatObject(actual, "Format-Table"));
            }
        }
    }
}
