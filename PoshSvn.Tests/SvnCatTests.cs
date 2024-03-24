// Copyright (c) Timofei Zhakov. All rights reserved.

using NUnit.Framework;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnCatTests
    {
        [Test]
        public void SimpleTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content -Path wc\a.txt -Value abc");
                sb.RunScript(@"svn-add wc\a.txt");
                sb.RunScript(@"svn-commit wc -m test");
                var actual = sb.RunScript(@"svn-cat wc\a.txt");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        "abc"
                    },
                    actual);
            }
        }

        [Test]
        public void MultilineTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content -Path wc\a.txt -Value a,b,c");
                sb.RunScript(@"svn-add wc\a.txt");
                sb.RunScript(@"svn-commit wc -m test");
                var actual = sb.RunScript(@"svn-cat wc\a.txt");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        "a",
                        "b",
                        "c"
                    },
                    actual);
            }
        }

        [Test]
        public void ByUrlTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content -Path wc\a.txt -Value abc");
                sb.RunScript(@"svn-add wc\a.txt");
                sb.RunScript(@"svn-commit wc -m test");
                var actual = sb.RunScript($@"svn-cat {sb.ReposUrl}/a.txt");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        "abc"
                    },
                    actual);
            }
        }

        [Test]
        public void AsByteStreamTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content -Path wc\a.txt -Value abc -NoNewline");
                sb.RunScript(@"svn-add wc\a.txt");
                sb.RunScript(@"svn-commit wc -m test");
                var actual = sb.RunScript($@"svn-cat wc/a.txt -AsByteStream");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        (byte)'a',
                        (byte)'b',
                        (byte)'c',
                    },
                    actual);
            }
        }
    }
}
