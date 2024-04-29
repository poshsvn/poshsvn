// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnPropsetCmdletTests
    {
        [Test]
        public void PropsetStatusTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset name value wc\test");
                var actual = sb.FormatObject(sb.RunScript(@"svn-status wc"), "Format-Table");
                CollectionAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"Status  Path",
                        @"------  ----",
                        @" M      wc\test",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void OutputAddTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                var actual = sb.RunScript(@"svn-propset name value wc\test");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "name",
                            Path = Path.Combine(sb.WcPath, "test"),
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void OutputModifyTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset name value wc\test");
                var actual = sb.RunScript(@"svn-propset name value2 wc\test");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "name",
                            Path = Path.Combine(sb.WcPath, "test"),
                        }
                    },
                    actual);
            }
        }
    }
}
