// Copyright (c) Timofei Zhakov. All rights reserved.

using NUnit.Framework;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnBlameCmdletTests
    {
        [Test]
        public void BasicTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content -Path wc\a.txt -Value line1,line2");
                sb.RunScript(@"svn-add wc\a.txt");
                sb.RunScript(@"svn-commit wc -m test");

                sb.RunScript(@"Set-Content -Path wc\a.txt -Value line1,line2,line3");
                sb.RunScript(@"svn-commit wc -m test");

                sb.RunScript(@"Set-Content -Path wc\a.txt -Value line1,'modified line2',line3");
                sb.RunScript(@"svn-commit wc -m test");

                var actual = sb.RunScript(@"svn-blame wc\a.txt");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnBlameLine
                        {
                            Revision = 1,
                            LineNumber = 0,
                            Line = "line1"
                        },
                        new SvnBlameLine
                        {
                            Revision = 3,
                            LineNumber = 1,
                            Line = "modified line2"
                        },
                        new SvnBlameLine
                        {
                            Revision = 2,
                            LineNumber = 2,
                            Line = "line3"
                        },
                    },
                    actual,
                    nameof(SvnBlameLine.Author));
            }
        }
    }
}
