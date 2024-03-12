// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.CmdLets;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnCommitTests
    {
        [Test]
        public void ManyTargets()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\a wc\b wc\c");

                sb.RunScript(@"svn-commit wc\a wc\b -m 'test'");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnLocalStatusOutput
                        {
                            LocalNodeStatus = SharpSvn.SvnStatus.Added,
                            Path = Path.Combine(sb.WcPath, "c"),
                            LocalTextStatus = SharpSvn.SvnStatus.Normal,
                            Versioned = true,
                            Conflicted = false,
                            LocalCopied = false,
                        },
                    },
                    sb.RunScript(@"svn-status wc\c"));
            }
        }

        [Test]
        public void OutputFormatterTest()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(
                    new[]
                    {
                        new SvnCommitOutput
                        {
                            Revision = 123
                        }
                    },
                    "Format-Custom");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        "",
                        "Committed revision 123.",
                        "",
                        "",
                    },
                    actual);
            }
        }

        [Test]
        public void WithRevpropOutputTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\a");

                var actual = sb.RunScript(@"svn-commit wc -m 'test' -revprop @{ prop = 'val' }");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.CommitAdded,
                            Path = Path.Combine(sb.WcPath, "a")
                        },
                        new SvnCommitOutput
                        {
                            Revision = 1
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void WithRevpropLogTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\a");
                sb.RunScript(@"svn-commit wc -m 'test' -revprop @{ prop = 'val' }");
                var actual = sb.FormatObject(
                    sb.RunScript($@"(svn-log -End 1 -WithRevisionProperties prop, svn:log {sb.ReposUrl}).RevisionProperties | foreach {{ ""$($_.Key) : $($_.StringValue)"" }}"),
                    "Format-Custom");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        "prop : val",
                        "svn:log : test",
                    },
                    actual);
            }
        }
    }
}
