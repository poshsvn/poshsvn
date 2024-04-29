// Copyright (c) Timofei Zhakov. All rights reserved.

using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnPropertyTests
    {
        [Test]
        public void OutputFormatTableTest()
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
                        @"Name Value Path",
                        @"---- ----- ----",
                        @"name       wc\test",
                        @"",
                        @"",
                    },
                    actual);
            }
        }
    }
}
