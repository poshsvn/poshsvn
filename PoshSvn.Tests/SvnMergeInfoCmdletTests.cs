// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnMergeInfoCmdletTests
    {
        [Test]
        public void EligibleAllTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > b.txt; svn-add b.txt; svn-commit -m 'add b.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > c.txt; svn-add c.txt; svn-commit -m 'add c.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > x.txt; svn-add x.txt; svn-commit -m 'add x.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > y.txt; svn-add y.txt; svn-commit -m 'add y.txt'");

                var actual = sb.RunScript($@"svn-mergeinfo '{sb.ReposUrl}/branches/test' '{sb.ReposUrl}/trunk' -ShowRevs Eligible");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnMergeInfoRevision { Revision = 3, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add a.txt" },
                        new SvnMergeInfoRevision { Revision = 4, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add b.txt" },
                        new SvnMergeInfoRevision { Revision = 5, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add c.txt" },
                        new SvnMergeInfoRevision { Revision = 6, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add x.txt" },
                        new SvnMergeInfoRevision { Revision = 7, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add y.txt" },
                    },
                    actual,
                    nameof(SvnMergeInfoRevision.Date),
                    nameof(SvnMergeInfoRevision.Author));
            }
        }

        [Test]
        public void SimpleLogTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > b.txt; svn-add b.txt; svn-commit -m 'add b.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > c.txt; svn-add c.txt; svn-commit -m 'add c.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > x.txt; svn-add x.txt; svn-commit -m 'add x.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > y.txt; svn-add y.txt; svn-commit -m 'add y.txt'");

                var actual = sb.RunScript($@"svn-mergeinfo '{sb.ReposUrl}/branches/test' '{sb.ReposUrl}/trunk' -ShowRevs Eligible -Log");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnLogOutput { Revision = 3, Message = "add a.txt" },
                        new SvnLogOutput { Revision = 4, Message = "add b.txt" },
                        new SvnLogOutput { Revision = 5, Message = "add c.txt" },
                        new SvnLogOutput { Revision = 6, Message = "add x.txt" },
                        new SvnLogOutput { Revision = 7, Message = "add y.txt" },
                    },
                    actual,
                    nameof(SvnLogOutput.Date),
                    nameof(SvnLogOutput.Author),
                    nameof(SvnLogOutput.RevisionProperties));
            }
        }

        [Test]
        public void RevisionsFormatTableTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > b.txt; svn-add b.txt; svn-commit -m 'add b.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > c.txt; svn-add c.txt; svn-commit -m 'add c.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > x.txt; svn-add x.txt; svn-commit -m 'add x.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > y.txt; svn-add y.txt; svn-commit -m 'add y.txt'");

                var actual = sb.RunScript($@"svn-mergeinfo '{sb.ReposUrl}/branches/test' '{sb.ReposUrl}/trunk' -ShowRevs Eligible");

                ClassicAssert.AreEqual(
                    new object[]
                    {
                        "",
                        "Revision",
                        "--------",
                        "r3",
                        "r4",
                        "r5",
                        "r6",
                        "r7",
                        "",
                        "",
                    },
                    sb.FormatObject(actual, "Format-Table"));
            }
        }

        [Test]
        public void EligibleNoneTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > b.txt; svn-add b.txt; svn-commit -m 'add b.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > c.txt; svn-add c.txt; svn-commit -m 'add c.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > x.txt; svn-add x.txt; svn-commit -m 'add x.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > y.txt; svn-add y.txt; svn-commit -m 'add y.txt'");

                var actual = sb.RunScript($@"svn-mergeinfo '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -ShowRevs Eligible");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        // What???
                        new SvnMergeInfoRevision { Revision = 2, SourceUri = new Uri($"{sb.ReposUrl}/branches/test"), LogMessage = "branch" },
                    },
                    actual,
                    nameof(SvnMergeInfoRevision.Date),
                    nameof(SvnMergeInfoRevision.Author));
            }
        }

        [Test]
        public void EligibleSomeTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > b.txt; svn-add b.txt; svn-commit -m 'add b.txt'");

                sb.RunScript($@"svn-switch '{sb.ReposUrl}/branches/test' wc-trunk");
                sb.RunScript($@"svn-merge '{sb.ReposUrl}/trunk' wc-trunk");
                sb.RunScript($@"svn-commit -m merge wc-trunk");
                sb.RunScript($@"svn-switch '{sb.ReposUrl}/trunk' wc-trunk");

                sb.RunScript($@"cd wc-trunk; 'abc' > c.txt; svn-add c.txt; svn-commit -m 'add c.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > x.txt; svn-add x.txt; svn-commit -m 'add x.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > y.txt; svn-add y.txt; svn-commit -m 'add y.txt'");

                var actual = sb.RunScript($@"svn-mergeinfo '{sb.ReposUrl}/branches/test' '{sb.ReposUrl}/trunk' -ShowRevs Eligible");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnMergeInfoRevision { Revision = 6, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add c.txt" },
                        new SvnMergeInfoRevision { Revision = 7, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add x.txt" },
                        new SvnMergeInfoRevision { Revision = 8, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add y.txt" },
                    },
                    actual,
                    nameof(SvnMergeInfoRevision.Date),
                    nameof(SvnMergeInfoRevision.Author));
            }
        }

        [Test]
        public void MergedSomeTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > b.txt; svn-add b.txt; svn-commit -m 'add b.txt'");

                sb.RunScript($@"svn-switch '{sb.ReposUrl}/branches/test' wc-trunk");
                sb.RunScript($@"svn-merge '{sb.ReposUrl}/trunk' wc-trunk");
                sb.RunScript($@"svn-commit -m merge wc-trunk");
                sb.RunScript($@"svn-switch '{sb.ReposUrl}/trunk' wc-trunk");

                sb.RunScript($@"cd wc-trunk; 'abc' > c.txt; svn-add c.txt; svn-commit -m 'add c.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > x.txt; svn-add x.txt; svn-commit -m 'add x.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > y.txt; svn-add y.txt; svn-commit -m 'add y.txt'");

                var actual = sb.RunScript($@"svn-mergeinfo '{sb.ReposUrl}/branches/test' '{sb.ReposUrl}/trunk' -ShowRevs Merged");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnMergeInfoRevision { Revision = 3, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add a.txt" },
                        new SvnMergeInfoRevision { Revision = 4, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add b.txt" },
                    },
                    actual,
                    nameof(SvnMergeInfoRevision.Date),
                    nameof(SvnMergeInfoRevision.Author));
            }
        }

        [Test]
        public void SimpleTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > b.txt; svn-add b.txt; svn-commit -m 'add b.txt'");

                sb.RunScript($@"svn-switch '{sb.ReposUrl}/branches/test' wc-trunk");
                sb.RunScript($@"svn-merge '{sb.ReposUrl}/trunk' wc-trunk");
                sb.RunScript($@"svn-commit -m merge wc-trunk");
                sb.RunScript($@"svn-switch '{sb.ReposUrl}/trunk' wc-trunk");

                sb.RunScript($@"cd wc-trunk; 'abc' > c.txt; svn-add c.txt; svn-commit -m 'add c.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > x.txt; svn-add x.txt; svn-commit -m 'add x.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > y.txt; svn-add y.txt; svn-commit -m 'add y.txt'");

                var actual = sb.RunScript($@"svn-mergeinfo '{sb.ReposUrl}/branches/test' '{sb.ReposUrl}/trunk'");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnMergeInfo
                        {
                            IsReintegration = false,
                            RepositoryRootUrl = new System.Uri(Path.Combine(sb.ReposPath, "")),
                            YoungestCommonAncestorUrl = new System.Uri(Path.Combine(sb.ReposPath, "trunk")),
                            YoungestCommonAncestorRevision = 1,
                            BaseUrl = new System.Uri(Path.Combine(sb.ReposPath, "trunk")),
                            BaseRevision = 4,
                            RightUrl = new System.Uri(Path.Combine(sb.ReposPath, "trunk")),
                            RightRevision = 8,
                            TargetUrl = new System.Uri(Path.Combine(sb.ReposPath, "branches/test")),
                            TargetRevision = 8,
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void MergedAllTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > b.txt; svn-add b.txt; svn-commit -m 'add b.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > c.txt; svn-add c.txt; svn-commit -m 'add c.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > x.txt; svn-add x.txt; svn-commit -m 'add x.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > y.txt; svn-add y.txt; svn-commit -m 'add y.txt'");

                sb.RunScript($@"svn-switch '{sb.ReposUrl}/branches/test' wc-trunk");
                sb.RunScript($@"svn-merge '{sb.ReposUrl}/trunk' wc-trunk");
                sb.RunScript($@"svn-commit -m merge wc-trunk");

                var actual = sb.RunScript($@"svn-mergeinfo '{sb.ReposUrl}/branches/test' '{sb.ReposUrl}/trunk' -ShowRevs Merged");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnMergeInfoRevision { Revision = 3, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add a.txt" },
                        new SvnMergeInfoRevision { Revision = 4, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add b.txt" },
                        new SvnMergeInfoRevision { Revision = 5, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add c.txt" },
                        new SvnMergeInfoRevision { Revision = 6, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add x.txt" },
                        new SvnMergeInfoRevision { Revision = 7, SourceUri = new Uri($"{sb.ReposUrl}/trunk"), LogMessage = "add y.txt" },
                    },
                    actual,
                    nameof(SvnMergeInfoRevision.Date),
                    nameof(SvnMergeInfoRevision.Author));
            }
        }

        [Test]
        public void MergedNoneTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > b.txt; svn-add b.txt; svn-commit -m 'add b.txt'");
                sb.RunScript($@"cd wc-trunk; 'abc' > c.txt; svn-add c.txt; svn-commit -m 'add c.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > x.txt; svn-add x.txt; svn-commit -m 'add x.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > y.txt; svn-add y.txt; svn-commit -m 'add y.txt'");

                var actual = sb.RunScript($@"svn-mergeinfo '{sb.ReposUrl}/branches/test' '{sb.ReposUrl}/trunk' -ShowRevs Merged");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                    },
                    actual);
            }
        }
    }
}
