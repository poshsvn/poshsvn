// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.CmdLets;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnRevertTests
    {
        [Test]
        public void BasicRevertOutput()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\a wc\b wc\c");
                var actual = sb.RunScript(@"svn-revert wc\a");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Revert,
                            Path = Path.Combine(sb.WcPath, "a")
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void BasicRevertStatus()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(
                    @"svn-mkdir wc\a wc\b wc\c",
                    @"svn-revert wc\a");

                var actual = sb.RunScript(
                    @"svn-status wc");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.NotVersioned,
                            Path = Path.Combine(sb.WcPath, "a"),
                            LocalTextStatus = SharpSvn.SvnStatus.None,
                            Versioned = false,
                            LocalCopied = false,
                        },
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.Added,
                            Path = Path.Combine(sb.WcPath, "b"),
                            LocalTextStatus = SharpSvn.SvnStatus.Normal,
                            Versioned = true,
                            LocalCopied = false,
                        },
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.Added,
                            Path = Path.Combine(sb.WcPath, "c"),
                            LocalTextStatus = SharpSvn.SvnStatus.Normal,
                            Versioned = true,
                            LocalCopied = false,
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void RecursiveRevertOutputTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\a wc\b wc\c");
                var actual = sb.RunScript(@"svn-revert wc -Depth Infinity");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Revert,
                            Path = Path.Combine(sb.WcPath, "a")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Revert,
                            Path = Path.Combine(sb.WcPath, "b")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Revert,
                            Path = Path.Combine(sb.WcPath, "c")
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void RecursiveRevertStatusTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(
                    @"svn-mkdir wc\a wc\b wc\c",
                    @"svn-revert wc -Recursive");

                var actual = sb.RunScript(
                    @"svn-status wc");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.NotVersioned,
                            Path = Path.Combine(sb.WcPath, "a"),
                            LocalTextStatus = SharpSvn.SvnStatus.None,
                            Versioned = false,
                            LocalCopied = false,
                        },
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.NotVersioned,
                            Path = Path.Combine(sb.WcPath, "b"),
                            LocalTextStatus = SharpSvn.SvnStatus.None,
                            Versioned = false,
                            LocalCopied = false,
                        },
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.NotVersioned,
                            Path = Path.Combine(sb.WcPath, "c"),
                            LocalTextStatus = SharpSvn.SvnStatus.None,
                            Versioned = false,
                            LocalCopied = false,
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void RemoveAddedTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(
                    @"svn-mkdir wc\a wc\b wc\c",
                    @"svn-revert wc -Recursive -RemoveAdded");

                var actual = sb.RunScript(
                    @"svn-status wc");

                PSObjectAssert.AreEqual(
                    new SvnLocalStatusOutput[]
                    {
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.NotVersioned,
                            Path = Path.Combine(sb.WcPath, "a"),
                            LocalTextStatus = SharpSvn.SvnStatus.None,
                            Versioned = false,
                            LocalCopied = false,
                        },
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.NotVersioned,
                            Path = Path.Combine(sb.WcPath, "b"),
                            LocalTextStatus = SharpSvn.SvnStatus.None,
                            Versioned = false,
                            LocalCopied = false,
                        },
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.NotVersioned,
                            Path = Path.Combine(sb.WcPath, "c"),
                            LocalTextStatus = SharpSvn.SvnStatus.None,
                            Versioned = false,
                            LocalCopied = false,
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void RevertOutputFormatterTest()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Revert,
                            Path = @"C:\path\to\wc\a",
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Revert,
                            Path = @"C:\path\to\wc\b",
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.Revert,
                            Path = @"C:\path\to\wc\c",
                        },
                    },
                    "Format-Table");

                CollectionAssert.AreEqual(
                    new[]
                    {
                        @"",
                        @"Action  Path",
                        @"------  ----",
                        @"Revert  C:\path\to\wc\a",
                        @"Revert  C:\path\to\wc\b",
                        @"Revert  C:\path\to\wc\c",
                        @"",
                        @"",
                    },
                    actual);
            }
        }
    }
}
