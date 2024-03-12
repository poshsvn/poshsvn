// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.CmdLets;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnStatusTests
    {
        [Test]
        public void SimpleTest()
        {
            using (var sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript($"(svn-status wc -All | Out-String -stream).TrimEnd()");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        $@"",
                        $@"Status  Path",
                        $@"------  ----",
                        $@"        wc",
                        $@"",
                        $@"",
                    },
                    Array.ConvertAll(actual.ToArray(), a => (string)a.BaseObject));
            }
        }

        [Test]
        public void CopyTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\a");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-copy wc\a wc\b");

                var actual = sb.RunScript(@"svn-status wc");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.Added,
                            Path = Path.Combine(sb.WcPath, "b"),
                            LocalTextStatus = SharpSvn.SvnStatus.Normal,
                            Versioned = true,
                            Conflicted = false,
                            LocalCopied = true,
                            LastChangedRevision = 1
                        },
                    },
                    actual,
                    nameof(SvnLocalStatusOutput.LastChangedAuthor),
                    nameof(SvnLocalStatusOutput.LastChangedTime));
            }
        }

        [Test]
        public void CopyOutputTest()
        {
            using (var sb = new PowerShellSandbox())
            {
                CollectionAssert.AreEqual(
                        new[]
                        {
                            "",
                            "Status  Path",
                            "------  ----",
                            "A  +    b",
                            "",
                            "",
                        },
                        sb.FormatObject(
                            new[]
                            {
                                new SvnLocalStatusOutput
                                {
                                    LocalNodeStatus = SharpSvn.SvnStatus.Added,
                                    Path = Path.Combine(sb.RootPath, "b"),
                                    LocalTextStatus = SharpSvn.SvnStatus.Normal,
                                    Versioned = true,
                                    Conflicted = false,
                                    LocalCopied = true,
                                    LastChangedRevision = 1
                                },
                            },
                            "Format-Table"));
            }
        }

        [Test]
        public void ManyTargets()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\a wc\b");

                var actual = sb.RunScript(@"svn-status wc\a wc\b");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.Added,
                            Path = Path.Combine(sb.WcPath, "a"),
                            LocalTextStatus = SharpSvn.SvnStatus.Normal,
                            Versioned = true,
                            Conflicted = false,
                            LocalCopied = false,
                        },
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.Added,
                            Path = Path.Combine(sb.WcPath, "b"),
                            LocalTextStatus = SharpSvn.SvnStatus.Normal,
                            Versioned = true,
                            Conflicted = false,
                            LocalCopied = false,
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void NewFileTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"'abc' > wc\a.txt");
                sb.RunScript(@"svn-add wc\a.txt");
                var actual = sb.RunScript(@"svn-status wc");

                CollectionAssert.AreEqual(
                       new[]
                       {
                            @"",
                            @"Status  Path",
                            @"------  ----",
                            @"A       wc\a.txt",
                            @"",
                            @"",
                       },
                       sb.FormatObject(actual, "Format-Table"));
            }
        }
    }
}
