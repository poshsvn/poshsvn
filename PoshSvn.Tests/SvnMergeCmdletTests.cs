// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
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

        [Test]
        public void SimpleFormatTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"svn-mkdir '{sb.ReposUrl}/branches/test/dir' -m test");
                sb.RunScript($@"svn-update wc-trunk");
                var actual = sb.FormatObject(sb.RunScript($@"svn-merge '{sb.ReposUrl}/branches/test' wc-trunk"), "Format-Custom");

                CollectionAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"A       wc-trunk\dir",
                        @"U       wc-trunk",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void SimpleConflictTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"svn-switch '{sb.ReposUrl}/branches/test' wc-trunk");
                sb.RunScript($@"cd wc-trunk; 'xyz' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"svn-update wc-trunk");

                Assert.Throws<SharpSvn.SvnWorkingCopyException>(() => sb.RunScript($@"svn-merge '{sb.ReposUrl}/trunk' wc-trunk"));
            }
        }

        [Test]
        public void ResolveConflictByAcceptParameterTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"svn-switch '{sb.ReposUrl}/branches/test' wc-trunk");
                sb.RunScript($@"cd wc-trunk; 'xyz' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"svn-update wc-trunk");

                var actual = sb.RunScript($@"svn-merge '{sb.ReposUrl}/trunk' wc-trunk -Accept Postpone");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnNotifyOutput { Action = SvnNotifyAction.TreeConflict, Path = Path.Combine(sb.TrunkPath, "a.txt") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.RecordMergeInfo, Path = Path.Combine(sb.TrunkPath) },
                        new SvnTreeConflictSummary { Action = SvnConflictAction.Add, FileName = "a.txt" },
                    },
                    actual);
            }
        }

        [Test]
        public void ManyConflictsTest()
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
                sb.RunScript($@"cd wc-trunk; 'xyz' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > b.txt; svn-add b.txt; svn-commit -m 'add b.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > c.txt; svn-add c.txt; svn-commit -m 'add c.txt'");
                sb.RunScript($@"cd wc-trunk; 'xyz' > z.txt; svn-add z.txt; svn-commit -m 'add z.txt'");
                sb.RunScript($@"svn-update wc-trunk");

                var actual = sb.FormatObject(sb.RunScript($@"svn-merge '{sb.ReposUrl}/trunk' wc-trunk -Accept Postpone"), "Format-Custom");

                CollectionAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"C       wc-trunk\a.txt",
                        @"C       wc-trunk\b.txt",
                        @"C       wc-trunk\c.txt",
                        @"A       wc-trunk\x.txt",
                        @"A       wc-trunk\y.txt",
                        @"U       wc-trunk",
                        @"Text conflict discovered in file 'a.txt'.",
                        @"Text conflict discovered in file 'b.txt'.",
                        @"Text conflict discovered in file 'c.txt'.",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void ResolveConflictByAcceptParameterFormatTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"svn-switch '{sb.ReposUrl}/branches/test' wc-trunk");
                sb.RunScript($@"cd wc-trunk; 'xyz' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"svn-update wc-trunk");

                var actual = sb.FormatObject(sb.RunScript($@"svn-merge '{sb.ReposUrl}/trunk' wc-trunk -Accept Postpone"), "Format-Custom");

                //svn.exe does the following:
                //---Merging r2 through r4 into '.':
                //   C a.txt
                //--- Recording mergeinfo for merge of r2 through r4 into '.':
                // U   .
                //Summary of conflicts:
                //  Tree conflicts: 1

                CollectionAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"C       wc-trunk\a.txt",
                        @"U       wc-trunk",
                        @"Text conflict discovered in file 'a.txt'.",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void ResolveTextConflictByAcceptParameterFormatTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"Set-Content wc-trunk/a.txt xyz; svn-commit wc-trunk -m test");
                sb.RunScript($@"svn-switch '{sb.ReposUrl}/branches/test' wc-trunk");
                sb.RunScript($@"Set-Content wc-trunk/a.txt qq");
                sb.RunScript($@"svn-update wc-trunk");

                var actual = sb.FormatObject(sb.RunScript($@"svn-merge '{sb.ReposUrl}/trunk' wc-trunk -Accept Postpone"), "Format-Custom");

                CollectionAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"U       wc-trunk\a.txt",
                        @"U       wc-trunk",
                        @"Text conflict discovered in file 'a.txt'.",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void MergeMoveTest()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"svn-switch '{sb.ReposUrl}/branches/test' wc-trunk");
                sb.RunScript($@"svn-move '{sb.ReposUrl}/trunk/a.txt' '{sb.ReposUrl}/trunk/b.txt' -m move");
                sb.RunScript($@"svn-update wc-trunk");

                var actual = sb.FormatObject(sb.RunScript($@"svn-merge '{sb.ReposUrl}/trunk' wc-trunk -Accept Postpone"), "Format-Custom");

                CollectionAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"A       wc-trunk\b.txt",
                        @"U       wc-trunk",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void DontAllowMixedRevisions()
        {
            using (var sb = new ProjectStructureSandbox())
            {
                sb.RunScript($@"svn-copy '{sb.ReposUrl}/trunk' '{sb.ReposUrl}/branches/test' -m branch");
                sb.RunScript($@"cd wc-trunk; 'abc' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");
                sb.RunScript($@"svn-switch '{sb.ReposUrl}/branches/test' wc-trunk");
                sb.RunScript($@"cd wc-trunk; 'xyz' > a.txt; svn-add a.txt; svn-commit -m 'add a.txt'");

                Assert.Throws<SharpSvn.SvnClientException>(() => sb.RunScript($@"svn-merge '{sb.ReposUrl}/branches/test' wc-trunk -Accept Postpone"));
            }
        }
    }
}
