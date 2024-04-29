// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnProplistCmdletTests
    {
        [Test]
        public void SimpleTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset name value wc\test");

                var actual = sb.RunScript(@"svn-proplist wc\test");

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
    }
}
