// Copyright (c) Timofei Zhakov. All rights reserved.

using NUnit.Framework;
using PoshSvn.Tests.TestUtils;
using System.IO;

namespace PoshSvn.Tests
{
    public class SvnPropdelCmdletTests
    {
        [Test]
        public void SimpleTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-propset name value wc");

                var actual = sb.RunScript(@"svn-propdel name wc");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.PropertyDeleted,
                            Path = sb.WcPath,
                        }
                    },
                    actual);
            }
        }
    }
}
