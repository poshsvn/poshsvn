// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnPropgetCmdletTests
    {
        [Test]
        public void SimpleTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset name value wc\test");

                var actual = sb.RunScript(@"svn-propget name wc\test");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "name",
                            Value = "value",
                            Path = Path.Combine(sb.WcPath, "test"),
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void NotExisting()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset name value wc\test");

                var actual = sb.RunScript(@"svn-propget not-exiting wc\test");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                    },
                    actual);
            }
        }

        [Test]
        public void MnayTargets()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\dir1");
                sb.RunScript(@"svn-mkdir wc\dir2");
                sb.RunScript(@"svn-mkdir wc\dir3");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset name value wc\dir1 wc\dir2");
                sb.RunScript(@"svn-propset name valueqq wc\dir3");

                var actual = sb.RunScript(@"svn-propget name wc\dir1 wc\dir2 wc\dir3");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "name",
                            Value = "value",
                            Path = Path.Combine(sb.WcPath, "dir1"),
                        },
                        new SvnProperty
                        {
                            Name = "name",
                            Value = "value",
                            Path = Path.Combine(sb.WcPath, "dir2"),
                        },
                        new SvnProperty
                        {
                            Name = "name",
                            Value = "valueqq",
                            Path = Path.Combine(sb.WcPath, "dir3"),
                        },
                    },
                    actual);
            }
        }
    }
}
