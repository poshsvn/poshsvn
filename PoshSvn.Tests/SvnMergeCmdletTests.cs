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

                var actual = sb.RunScript($@"svn-merge '{sb.ReposUrl}/branches/test' wc-trunk -Accept Postpone");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnNotifyOutput { Action = SvnNotifyAction.TreeConflict, Path = Path.Combine(sb.TrunkPath, "a.txt") },
                        new SvnNotifyOutput { Action = SvnNotifyAction.RecordMergeInfo, Path = Path.Combine(sb.TrunkPath) },
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

                var actual = sb.FormatObject(sb.RunScript($@"svn-merge '{sb.ReposUrl}/branches/test' wc-trunk -Accept Postpone"), "Format-Custom");

                CollectionAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"C       wc-trunk\a.txt",
                        @"U       wc-trunk",
                        @"",
                        @"",
                    },
                    actual);
            }
        }
    }
}
