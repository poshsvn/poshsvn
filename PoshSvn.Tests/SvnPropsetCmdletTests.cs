// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
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
        public void CurrentDirectoryTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"cd wc\test; svn-propset name value");
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
                            Value = "value",
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
                            Value = "value2",
                            Path = Path.Combine(sb.WcPath, "test"),
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void ThrowOnRemoteTarget()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                Assert.Throws<ArgumentException>(() => sb.RunScript($@"svn-propset name value {sb.ReposUrl}"));
            }
        }

        [Test]
        public void RevpropNoPreRevpropChangeHookTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-propset name value1 wc");
                sb.RunScript(@"svn-commit wc -m test");

                sb.RunScript(@"svn-propset name value2 wc");
                sb.RunScript(@"svn-commit wc -m test");

                sb.RunScript(@"svn-propset name value3 wc");
                sb.RunScript(@"svn-commit wc -m test");

                Assert.Throws<SharpSvn.SvnRepositoryException>(() =>
                sb.RunScript($@"svn-propset svn:log 'new log message' {sb.ReposUrl} -revprop -r 2 "));
            }
        }

        [Test]
        public void SimpleRevpropTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-propset name value1 wc");
                sb.RunScript(@"svn-commit wc -m test");

                sb.RunScript(@"svn-propset name value2 wc");
                sb.RunScript(@"svn-commit wc -m test");

                sb.RunScript(@"svn-propset name value3 wc");
                sb.RunScript(@"svn-commit wc -m test");

                sb.EnableRevpropChange();

                var actual = sb.RunScript($@"svn-propset svn:log 'new log message' {sb.ReposUrl} -revprop -r 2 ");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "svn:log",
                            Value = "new log message",
                            Path = sb.ReposUrl,
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void SimpleRevpopTestByPropget()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-propset name value1 wc");
                sb.RunScript(@"svn-commit wc -m test");

                sb.RunScript(@"svn-propset name value2 wc");
                sb.RunScript(@"svn-commit wc -m test");

                sb.RunScript(@"svn-propset name value3 wc");
                sb.RunScript(@"svn-commit wc -m test");

                sb.EnableRevpropChange();

                sb.RunScript($@"svn-propset svn:log 'new log message' {sb.ReposUrl} -revprop -r 2 ");

                var actual = sb.RunScript($@"svn-propget svn:log {sb.ReposUrl} -revprop -r 2 ");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "svn:log",
                            Value = "new log message",
                            Path = sb.ReposUrl,
                        }
                    },
                    actual);
            }
        }
    }
}
