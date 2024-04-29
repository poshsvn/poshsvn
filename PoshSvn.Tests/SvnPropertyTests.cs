// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnPropertyTests
    {
        [Test]
        public void OutputFormatTableFromPropsetTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                var actual = sb.FormatObject(sb.RunScript(@"svn-propset name value wc\test"), "Format-Table");

                ClassicAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"",
                        @"    Properties on 'wc\test':",
                        @"",
                        @"Name",
                        @"----",
                        @"name",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void ManyProperties()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(new object[]
                {
                    new SvnProperty
                    {
                        Name = "name",
                        Path = Path.Combine(sb.WcPath, "test"),
                    },
                    new SvnProperty
                    {
                        Name = "foo",
                        Path = Path.Combine(sb.WcPath, "test"),
                    },
                    new SvnProperty
                    {
                        Name = "svn:mergeinfo",
                        Path = Path.Combine(sb.WcPath, "test"),
                    },
                },
                "Format-Table");

                ClassicAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"",
                        @"    Properties on 'wc\test':",
                        @"",
                        @"Name",
                        @"----",
                        @"name",
                        @"foo",
                        @"svn:mergeinfo",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void ManyGroups()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(new object[]
                {
                    new SvnProperty
                    {
                        Name = "svn:ignore",
                        Path = Path.Combine(sb.WcPath),
                    },
                    new SvnProperty
                    {
                        Name = "svn:mergeinfo",
                        Path = Path.Combine(sb.WcPath),
                    },
                    new SvnProperty
                    {
                        Name = "svn:ignore",
                        Path = Path.Combine(sb.WcPath, "Project1"),
                    },
                    new SvnProperty
                    {
                        Name = "foo",
                        Path = Path.Combine(sb.WcPath, "test"),
                    },
                    new SvnProperty
                    {
                        Name = "bar",
                        Path = Path.Combine(sb.WcPath, "test"),
                    },
                },
                "Format-Table");

                ClassicAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"",
                        @"    Properties on 'wc':",
                        @"",
                        @"Name",
                        @"----",
                        @"svn:ignore",
                        @"svn:mergeinfo",
                        @"",
                        @"",
                        @"    Properties on 'wc\Project1':",
                        @"",
                        @"Name",
                        @"----",
                        @"svn:ignore",
                        @"",
                        @"",
                        @"    Properties on 'wc\test':",
                        @"",
                        @"Name",
                        @"----",
                        @"foo",
                        @"bar",
                        @"",
                        @"",
                    },
                    actual);
            }
        }
    }
}
