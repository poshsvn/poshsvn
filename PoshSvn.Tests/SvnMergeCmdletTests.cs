// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnMergeCmdletTests
    {
        [Test]
        public void SimpleTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"svn-mkdir '{sb.ReposUrl}/branches/test/dir' -m test");
                sb.RunScript($@"svn-update wc-trunk");
                var actual = sb.RunScript($@"svn-merge '{sb.ReposUrl}/branches/test' wc-trunk");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnNotifyOutput { Action = SvnNotifyAction.UpdateAdd, Path = Path.Combine(sb.TrunkPath, "dir") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.RecordMergeInfo, Path = Path.Combine(sb.TrunkPath) },
                    },
                    actual);
            }
        }
    }
}
