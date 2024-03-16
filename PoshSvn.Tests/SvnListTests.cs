using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;
using SharpSvn;

namespace PoshSvn.Tests
{
    public class SvnListTests
    {
        [Test]
        public void BasicTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("cd wc; 1..3 | svn-mkdir; svn-commit -m init");
                var actual = sb.RunScript($"svn-list {sb.ReposUrl}");

                PSObjectAssert.AreEqual(
                    new SvnItem[]
                    {
                        new SvnItem
                        {
                            Path = "1",
                            NodeKind = SvnNodeKind.Directory
                        },
                        new SvnItem
                        {
                            Path = "2",
                            NodeKind = SvnNodeKind.Directory
                        },
                        new SvnItem
                        {
                            Path = "3",
                            NodeKind = SvnNodeKind.Directory
                        },
                    },
                    actual,
                    nameof(SvnItem.Date),
                    nameof(SvnItem.Uri),
                    nameof(SvnItem.ExternalTarget),
                    nameof(SvnItem.ExternalParent),
                    nameof(SvnItem.BasePath),
                    nameof(SvnItem.RepositoryRoot),
                    nameof(SvnItem.Name),
                    nameof(SvnItem.BaseUri));
            }
        }

        [Test]
        public void InfinityDepth()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("cd wc; svn-mkdir a/b/c d/e f/x/y/z/1/2/3 -Parents; svn-commit -m init");
                var actual = sb.RunScript($"svn-list {sb.ReposUrl} -Depth Infinity");

                PSObjectAssert.AreEqual(
                    new SvnItem[]
                    {
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "a" },
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "a/b" },
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "a/b/c" },
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "d" },
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "d/e" },
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "f" },
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "f/x" },
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "f/x/y" },
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "f/x/y/z" },
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "f/x/y/z/1" },
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "f/x/y/z/1/2" },
                        new SvnItem { NodeKind = SvnNodeKind.Directory, Path = "f/x/y/z/1/2/3" },
                    },
                    actual,
                    nameof(SvnItem.Date),
                    nameof(SvnItem.Uri),
                    nameof(SvnItem.ExternalTarget),
                    nameof(SvnItem.ExternalParent),
                    nameof(SvnItem.BasePath),
                    nameof(SvnItem.RepositoryRoot),
                    nameof(SvnItem.Name),
                    nameof(SvnItem.BaseUri));
            }
        }

        [Test]
        public void BasicTestDetailed()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript("cd wc; 1..3 | svn-mkdir; svn-commit -m init");
                var actual = sb.RunScript($"svn-list {sb.ReposUrl} -Detailed");

                PSObjectAssert.AreEqual(
                    new SvnItemDetailed[]
                    {
                        new SvnItemDetailed
                        {
                            Path = "",
                            Revision = 1,
                            FileSize = null,
                            NodeKind = SvnNodeKind.Directory
                        },
                        new SvnItemDetailed
                        {
                            Path = "1",
                            Revision = 1,
                            FileSize = null,
                            NodeKind = SvnNodeKind.Directory
                        },
                        new SvnItemDetailed
                        {
                            Path = "2",
                            Revision = 1,
                            FileSize = null,
                            NodeKind = SvnNodeKind.Directory
                        },
                        new SvnItemDetailed
                        {
                            Path = "3",
                            Revision = 1,
                            FileSize = null,
                            NodeKind = SvnNodeKind.Directory
                        },
                    },
                    actual,
                    nameof(SvnItemDetailed.Author),
                    nameof(SvnItemDetailed.Date),

                    nameof(SvnItemDetailed.Uri),
                    nameof(SvnItemDetailed.ExternalTarget),
                    nameof(SvnItemDetailed.ExternalParent),
                    nameof(SvnItemDetailed.BasePath),
                    nameof(SvnItemDetailed.RepositoryRoot),
                    nameof(SvnItemDetailed.Name),
                    nameof(SvnItemDetailed.BaseUri));
            }
        }

        [Test]
        public void FormatTest()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(
                    new SvnItem[]
                    {
                        new SvnItem
                        {
                            Path = "docs",
                            NodeKind = SvnNodeKind.Directory
                        },
                        new SvnItem
                        {
                            Path = "build",
                            NodeKind = SvnNodeKind.Directory
                        },
                        new SvnItem
                        {
                            Path = "src",
                            NodeKind = SvnNodeKind.Directory
                        },
                        new SvnItem
                        {
                            Path = "LICENSE",
                            NodeKind = SvnNodeKind.File
                        },
                        new SvnItem
                        {
                            Path = "README.md",
                            NodeKind = SvnNodeKind.File
                        }
                    },
                    "Format-Table");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        "",
                        "Path",
                        "----",
                        "docs/",
                        "build/",
                        "src/",
                        "LICENSE",
                        "README.md",
                        "",
                        "",
                    },
                    actual);
            }
        }

        [Test]
        public void FormatTestDetailed()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(
                    new SvnItemDetailed[]
                    {
                        new SvnItemDetailed
                        {
                            Path = "docs",
                            Revision = 23712,
                            Author = "John.Doe",
                            FileSize = null,
                            NodeKind = SvnNodeKind.Directory,
                            Date = DateTime.Parse("Jun 07  2023"),
                            HasProperties = true,
                        },
                        new SvnItemDetailed
                        {
                            Path = "build",
                            Revision = 23712,
                            Author = "John.Doe",
                            FileSize = null,
                            NodeKind = SvnNodeKind.Directory,
                            Date = DateTime.Parse("Jun 07  2023"),
                            HasProperties = true,
                        },
                        new SvnItemDetailed
                        {
                            Path = "src",
                            Revision = 23712,
                            Author = "John.Doe",
                            FileSize = null,
                            NodeKind = SvnNodeKind.Directory,
                            Date = DateTime.Parse("Jun 07  2023"),
                            HasProperties = true,
                        },
                        new SvnItemDetailed
                        {
                            Path = "LICENSE",
                            Revision = 28842,
                            Author = "Richard.Roe",
                            FileSize = 79842,
                            NodeKind = SvnNodeKind.File,
                            Date = DateTime.Parse("Dec 12  2022"),
                            HasProperties = true,
                        },
                        new SvnItemDetailed
                        {
                            Path = "README.md",
                            Revision = 28842,
                            Author = "Richard.Roe",
                            FileSize = 79842,
                            NodeKind = SvnNodeKind.File,
                            Date = DateTime.Parse("Dec 12  2022"),
                            HasProperties = true,
                        }
                    },
                    "Format-Table");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        "",
                        "Revision Author       FileSize Date        Path",
                        "-------- ------       -------- ----        ----",
                        "   23712 John.Doe              Jun 07 2023 docs/",
                        "   23712 John.Doe              Jun 07 2023 build/",
                        "   23712 John.Doe              Jun 07 2023 src/",
                        "   28842 Richard.Roe     79842 Dec 12 2022 LICENSE",
                        "   28842 Richard.Roe     79842 Dec 12 2022 README.md",
                        "",
                        "",
                    },
                    actual);
            }
        }
    }
}
