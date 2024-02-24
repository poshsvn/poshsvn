using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.CmdLets;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnLogTests
    {
        [Test]
        public void LogByPath()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(
                    @"svn-mkdir wc\a wc\b",
                    @"svn-commit wc\a -m 'test 1'",
                    @"svn-commit wc\b -m 'test 2'");

                var actual = sb.RunScript("svn-log wc");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnLogOutput
                        {
                            Revision = 2,
                            Message = "test 2",
                        },
                        new SvnLogOutput
                        {
                            Revision = 1,
                            Message = "test 1",
                        },
                        new SvnLogOutput
                        {
                            Revision = 0,
                            Message = null,
                        },
                    },
                    actual,
                    nameof(SvnLogOutput.Author),
                    nameof(SvnLogOutput.Date));
            }
        }

        [Test]
        public void LogByCurrentDirectory()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(
                    @"svn-mkdir wc\a wc\b",
                    @"svn-commit wc\a -m 'test 1'",
                    @"svn-commit wc\b -m 'test 2'");

                var actual = sb.RunScript($"svn-log {sb.ReposUrl}");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnLogOutput
                        {
                            Revision = 2,
                            Message = "test 2",
                        },
                        new SvnLogOutput
                        {
                            Revision = 1,
                            Message = "test 1",
                        },
                        new SvnLogOutput
                        {
                            Revision = 0,
                            Message = null,
                        },
                    },
                    actual,
                    nameof(SvnLogOutput.Author),
                    nameof(SvnLogOutput.Date));
            }
        }

        [Test]
        public void LogByUrl()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(
                    @"svn-mkdir wc\a wc\b",
                    @"svn-commit wc\a -m 'test 1'",
                    @"svn-commit wc\b -m 'test 2'");

                var actual = sb.RunScript($"cd wc; svn-log");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnLogOutput
                        {
                            Revision = 2,
                            Message = "test 2",
                        },
                        new SvnLogOutput
                        {
                            Revision = 1,
                            Message = "test 1",
                        },
                        new SvnLogOutput
                        {
                            Revision = 0,
                            Message = null,
                        },
                    },
                    actual,
                    nameof(SvnLogOutput.Author),
                    nameof(SvnLogOutput.Date));
            }
        }

        [Test]
        public void FormatCustomTest()
        {
            using (var sb = new WcSandbox())
            {
                var input = new[]
                    {
                        new SvnLogOutput
                        {
                            Revision = 2,
                            Message = "test 2",
                            Author = "user1",
                            Date = new DateTime(2021, 11, 5)
                        },
                        new SvnLogOutput
                        {
                            Revision = 1,
                            Message =
                                "On the 'pristine-checksum-salt' branch: Update BRANCH-README.\n" +
                                "\n" +
                                "* BRANCH-README: Tweak the section about pristine checksum kinds.\n" +
                                "  Add a section about using the dynamically salted SHA-1.",
                            Author = "user1",
                            Date = new DateTime(2021, 11, 6)
                        },
                        new SvnLogOutput
                        {
                            Revision = 0,
                            Message = null,
                            Author = null
                        },
                    };

                var actual = sb.FormatObject(input, "Format-Custom");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        "",
                        "------------------------------------------------------------------------",
                        "r2         user1               2021-11-05 12:00 +01:00",
                        "",
                        "test 2",
                        "------------------------------------------------------------------------",
                        "r1         user1               2021-11-06 12:00 +01:00",
                        "",
                        "On the 'pristine-checksum-salt' branch: Update BRANCH-README.",
                        "",
                        "* BRANCH-README: Tweak the section about pristine checksum kinds.",
                        "  Add a section about using the dynamically salted SHA-1.",
                        "------------------------------------------------------------------------",
                        "r0         (no author)         (no date)",
                        "",
                        "",
                        "",
                        "",
                    },
                    actual);
            }
        }

        [Test]
        public void FormatTableTest()
        {
            using (var sb = new WcSandbox())
            {
                var input = new[]
                    {
                        new SvnLogOutput
                        {
                            Revision = 3,
                            Message = "test 2",
                            Author = "user1",
                            Date = new DateTime(2021, 11, 5)
                        },
                        new SvnLogOutput
                        {
                            Revision = 2,
                            Message =
                                "On the 'pristine-checksum-salt' branch: Update BRANCH-README.\n" +
                                "\n" +
                                "* BRANCH-README: Tweak the section about pristine checksum kinds.\n" +
                                "  Add a section about using the dynamically salted SHA-1.",
                            Author = "user2",
                            Date = new DateTime(2021, 11, 6)
                        },
                        new SvnLogOutput
                        {
                            Revision = 1,
                            Message =
                                "The\n" +
                                "long\n" +
                                "\n" +
                                "and\n" +
                                "multiline\n" +
                                "log\n" +
                                "message",
                            Author = "user2",
                            Date = new DateTime(2021, 11, 6)
                        },
                        new SvnLogOutput
                        {
                            Revision = 0,
                            Message = null,
                            Author = null
                        },
                    };

                var actual = sb.FormatObject(input, "Format-Table");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        "",
                        "  Revision Author           Date                   Message",
                        "  -------- ------           ----                   -------",
                        "         3 user1            2021-11-05    12:00    test 2",
                        "         2 user2            2021-11-06    12:00    On the 'pristine-checksum-salt' branch: Update BRANCH-README.  * B...",
                        "         1 user2            2021-11-06    12:00    The long  and multiline log message",
                        "         0                  (no date)",
                        "",
                        "",
                    },
                    actual);
            }
        }
    }
}
