// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
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

        [Test]
        public void FormatTest()
        {
            using (var sb = new PowerShellSandbox())
            {
                var actual = sb.FormatObject(
                    new[]
                    {
                        new SvnBlameLine
                        {
                            Revision = 1,
                            LineNumber = 0,
                            Line = "line1",
                            Author = "sally",
                        },
                        new SvnBlameLine
                        {
                            Revision = 3,
                            LineNumber = 1,
                            Line = "modified line2",
                            Author = "harry"
                        },
                        new SvnBlameLine
                        {
                            Revision = 2,
                            LineNumber = 2,
                            Line = "line3",
                            Author = "sally"
                        },
                        new SvnBlameLine
                        {
                            Revision = 5,
                            LineNumber = 3,
                            Line = "line4",
                            Author = "John.Doe"
                        },
                        new SvnBlameLine
                        {
                            Revision = 5,
                            LineNumber = 4,
                            Line = "line5",
                            Author = "John.Doe"
                        },
                    },
                    "Format-Custom");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        "",
                        "       r1 sally            line1",
                        "       r3 harry            modified line2",
                        "       r2 sally            line3",
                        "       r5 John.Doe         line4",
                        "       r5 John.Doe         line5",
                        "",
                        "",
                    },
                    actual);
            }
        }
    }
}
