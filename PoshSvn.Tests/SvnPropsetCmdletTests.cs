// Copyright (c) Timofei Zhakov. All rights reserved.

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
    }
}
