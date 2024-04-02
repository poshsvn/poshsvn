// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Text;
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

        [Test]
        public void RawTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content -Path wc\a.txt -Value a,b,c");
                sb.RunScript(@"svn-add wc\a.txt");
                sb.RunScript(@"svn-commit wc -m test");
                var actual = sb.RunScript(@"svn-cat wc\a.txt -Raw");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        "a\r\n" +
                        "b\r\n" +
                        "c\r\n" +
                        ""
                    },
                    actual);
            }
        }

        [Test]
        public void RawWithAsByteStreamTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"Set-Content -Path wc\a.txt -Value a,b,c");
                sb.RunScript(@"svn-add wc\a.txt");
                sb.RunScript(@"svn-commit wc -m test");
                var actual = sb.RunScript(@"svn-cat wc\a.txt -Raw -AsByteStream");

                PSObjectAssert.AreEqual(
                    Encoding.UTF8.GetBytes("a\r\n" +
                                           "b\r\n" +
                                           "c\r\n" +
                                           ""),
                    actual);
            }
        }
    }
}
