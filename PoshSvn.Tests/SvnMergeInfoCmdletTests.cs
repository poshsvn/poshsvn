// Copyright (c) Timofei Zhakov. All rights reserved.

using NUnit.Framework;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnMergeInfoCmdletTests
    {
        [Test]
        public void EligibleTestAll()
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
                        new SvnMergeInfo { Revision = 3 },
                        new SvnMergeInfo { Revision = 4 },
                        new SvnMergeInfo { Revision = 5 },
                        new SvnMergeInfo { Revision = 6 },
                        new SvnMergeInfo { Revision = 7 },
                    },
                    actual);
            }
        }
        [Test]
        public void EligibleTestNone()
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
                        new SvnMergeInfo { Revision = 2 },
                    },
                    actual);
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
                        new SvnMergeInfo { Revision = 6 },
                        new SvnMergeInfo { Revision = 7 },
                        new SvnMergeInfo { Revision = 8 },
                    },
                    actual);
            }
        }
    }
}
