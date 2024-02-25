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
                            Date = new DateTimeOffset(2021, 11, 5, 12, 34, 56, TimeSpan.FromHours(3))
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
                            Date = new DateTimeOffset(2021, 11, 6, 12, 34, 56, TimeSpan.FromHours(3))
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
                        "r2         user1               2021-11-05 12:34 +03:00",
                        "",
                        "test 2",
                        "------------------------------------------------------------------------",
                        "r1         user1               2021-11-06 12:34 +03:00",
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
        public void FormatCustomWithChangedPathsTest()
        {
            using (var sb = new WcSandbox())
            {
                var input = new[]
                    {
                        new SvnLogOutput
                        {
                            Revision = 1232,
                            Message = "Some fixes",
                            Author = "user1",
                            Date = new DateTimeOffset(2021, 11, 5, 12, 34, 56, TimeSpan.FromHours(3)),
                            ChangedPaths = new[]
                            {
                                new SvnChangeItem
                                {
                                    Action = SvnChangeAction.Modify,
                                    ContentModified = true,
                                    PropertiesModified = false,
                                    CopyFromPath = null,
                                    CopyFromRevision = null,
                                    NodeKind = SharpSvn.SvnNodeKind.File,
                                    Path = "/qqq/def",
                                },
                                new SvnChangeItem
                                {
                                    Action = SvnChangeAction.Modify,
                                    ContentModified = true,
                                    PropertiesModified = false,
                                    CopyFromPath = null,
                                    CopyFromRevision = null,
                                    NodeKind = SharpSvn.SvnNodeKind.File,
                                    Path = "/qqq/xyz",
                                }
                            }
                        },
                        new SvnLogOutput
                        {
                            Revision = 1230,
                            Message = "Rename 'def' to 'xyz'",
                            Author = "user1",
                            Date = new DateTimeOffset(2021, 11, 6, 12, 34, 56, TimeSpan.FromHours(3)),
                            ChangedPaths = new[]
                            {
                                new SvnChangeItem
                                {
                                    Action = SvnChangeAction.Delete,
                                    ContentModified = false,
                                    PropertiesModified = false,
                                    CopyFromPath = null,
                                    CopyFromRevision = null,
                                    NodeKind = SharpSvn.SvnNodeKind.File,
                                    Path = "/abc/def",
                                },
                                new SvnChangeItem
                                {
                                    Action = SvnChangeAction.Add,
                                    ContentModified = false,
                                    PropertiesModified = false,
                                    CopyFromPath = null,
                                    CopyFromRevision = null,
                                    NodeKind = SharpSvn.SvnNodeKind.File,
                                    Path = "/abc/xyz",
                                }
                            }
                        },
                    };

                var actual = sb.FormatObject(input, "Format-Custom");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        "",
                        "------------------------------------------------------------------------",
                        "r1232      user1               2021-11-05 12:34 +03:00",
                        "   M /qqq/def",
                        "   M /qqq/xyz",
                        "",
                        "Some fixes",
                        "------------------------------------------------------------------------",
                        "r1230      user1               2021-11-06 12:34 +03:00",
                        "   D /abc/def",
                        "   A /abc/xyz",
                        "",
                        "Rename 'def' to 'xyz'",
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
                            Date = new DateTimeOffset (2021, 11, 7, 12, 34, 56, TimeSpan.FromHours(3))
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
                            Date = new DateTimeOffset (2021, 11, 8, 12, 34, 56, TimeSpan.FromHours(3))
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
                        "         3 user1            2021-11-07    12:34    test 2",
                        "         2 user2            2021-11-08    12:34    On the 'pristine-checksum-salt' branch: Update BRANCH-README.  * B...",
                        "         1 user2            2021-11-06    12:00    The long  and multiline log message",
                        "         0                  (no date)",
                        "",
                        "",
                    },
                    actual);
            }
        }

        [Test]
        public void WithChangedPaths()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(
                   @"svn-mkdir wc\a wc\b",
                   @"svn-commit wc -m 'test'");

                var actual = sb.RunScript("svn-log wc -v");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnLogOutput
                        {
                            Revision = 1,
                            Message = "test",
                        },
                        new SvnLogOutput
                        {
                            Revision = 0,
                        },
                    },
                    actual,
                    nameof(SvnLogOutput.Author),
                    nameof(SvnLogOutput.Date),
                    nameof(SvnLogOutput.ChangedPaths)); // TODO: check ChangedPaths
            }
        }

        [Test]
        public void Range()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(
                    @"
                    cd wc;
                    0..10 | foreach {
                        svn-mkdir $_;
                        svn-commit $_ -m 'test';
                    }");

                var actual = sb.RunScript("svn-log wc -start 5 -end 7");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnLogOutput
                        {
                            Revision = 5,
                            Message = "test",
                        },
                        new SvnLogOutput
                        {
                            Revision = 6,
                            Message = "test",
                        },
                        new SvnLogOutput
                        {
                            Revision = 7,
                            Message = "test",
                        },
                    },
                    actual,
                    nameof(SvnLogOutput.Author),
                    nameof(SvnLogOutput.Date));
            }
        }
    }
}
