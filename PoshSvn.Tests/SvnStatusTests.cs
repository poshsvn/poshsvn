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
    }
}
