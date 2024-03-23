// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using PoshSvn.CmdLets;
using PoshSvn.Tests.TestUtils;
using SharpSvn;

namespace PoshSvn.Tests
{
    public class SvnImportTests
    {
        [Test]
        public void ImportFile()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($@"mkdir tmp; Set-Content -Path tmp\a.txt -Value abc");
                var actual = sb.RunScript($@"svn-import tmp\a.txt {sb.ReposUrl}/a.txt -m test");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.CommitAdded,
                            Path = Path.Combine(sb.RootPath, @"tmp\a.txt")
                        },
                        new SvnCommitOutput
                        {
                            Revision = 1
                        },
                    },
                    actual);
            }
        }
        [Test]
        public void ImportDirectory()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($@"mkdir tmp");
                sb.RunScript($@"Set-Content -Path tmp\a.txt -Value abc");
                sb.RunScript($@"Set-Content -Path tmp\b.txt -Value abc");
                sb.RunScript($@"mkdir tmp\x");
                sb.RunScript($@"Set-Content -Path tmp\x\c.txt -Value abc");

                var actual = sb.RunScript($@"svn-import tmp {sb.ReposUrl}/imported -m test");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.CommitAdded,
                            Path = Path.Combine(sb.RootPath, @"tmp\a.txt")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.CommitAdded,
                            Path = Path.Combine(sb.RootPath, @"tmp\b.txt")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.CommitAdded,
                            Path = Path.Combine(sb.RootPath, @"tmp\x")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.CommitAdded,
                            Path = Path.Combine(sb.RootPath, @"tmp\x\c.txt")
                        },
                        new SvnCommitOutput
                        {
                            Revision = 1
                        },
                    },
                    actual);
            }
        }
    }
}
