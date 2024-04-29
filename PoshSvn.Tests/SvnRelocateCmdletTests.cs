// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using NUnit.Framework;
using PoshSvn.CmdLets;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnRelocateCmdletTests
    {
        [Test]
        public void NotAbsoluteUriTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("Copy-Item repos repos-new");
                Assert.Throws<ArgumentException>(() => sb.RunScript("svn-relocate -From repos -To repos-new -Path wc"));
            }
        }

        [Test]
        public void SimpleTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("Copy-Item repos repos-new -Recurse");
                sb.RunScript($"svn-relocate -From {sb.RootPath}/repos -To {sb.RootPath}/repos-new -Path wc");

                var info = sb.RunScript("svn-info wc");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnInfoOutput
                        {
                            Schedule = SharpSvn.SvnSchedule.Normal,
                            WorkingCopyRoot = sb.WcPath,
                            Path = sb.WcPath,
                            Url = new Uri(sb.RootPath + "/repos-new/"),
                            RelativeUrl = new Uri("", UriKind.Relative),
                            RepositoryRoot = new Uri(sb.RootPath + "/repos-new/"),
                            NodeKind = SvnNodeKind.Directory,
                            LastChangedAuthor = null,
                        },
                    },
                    info,
                    nameof(SvnInfoOutput.RepositoryId),
                    nameof(SvnInfoOutput.LastChangedDate),
                    nameof(SvnInfoOutput.RepositoryId));
            }
        }
    }
}
