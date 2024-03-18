// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using PoshSvn.Tests.TestUtils;
using SharpSvn;

namespace PoshSvn.Tests
{
    public class SvnExportTests
    {
        [Test]
        public void ExportOneItem()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($@"Set-Content -Path wc\a.txt -Value abc; svn-add wc\a.txt");
                sb.RunScript($@"svn-commit wc -m test");
                var actual = sb.RunScript($@"svn-export {sb.ReposUrl}/a.txt a.txt");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"a.txt")
                        },
                    },
                    actual);

                Assert.Throws<SvnSystemException>(() => sb.RunScript($@"svn-mkdir wc\a\b\c"));
            }
        }

        [Test]
        public void ExportOneItemAtPegRevision()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($@"Set-Content -Path wc\a.txt -Value abc; svn-add wc\a.txt");
                sb.RunScript($@"svn-commit wc -m test");
                sb.RunScript($@"svn-delete wc\a.txt");
                sb.RunScript($@"svn-commit wc -m test");

                var actual = sb.RunScript($@"svn-export {sb.ReposUrl}/a.txt@1 a.txt");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"a.txt")
                        },
                    },
                    actual);

                Assert.Throws<SvnSystemException>(() => sb.RunScript($@"svn-mkdir wc\a\b\c"));
            }
        }

        [Test]
        public void ExportOneItemAtRevisionParameter()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($@"Set-Content -Path wc\a.txt -Value abc; svn-add wc\a.txt");
                sb.RunScript($@"svn-commit wc -m test");
                sb.RunScript($@"svn-delete wc\a.txt");
                sb.RunScript($@"svn-commit wc -m test");

                var actual = sb.RunScript($@"svn-export {sb.ReposUrl}/a.txt a.txt -Revision 1");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"a.txt")
                        },
                    },
                    actual);

                Assert.Throws<SvnSystemException>(() => sb.RunScript($@"svn-mkdir wc\a\b\c"));
            }
        }

        [Test]
        public void ExportDirectory()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($@"Set-Content -Path wc\a.txt -Value abc; svn-add wc\a.txt");
                sb.RunScript($@"Set-Content -Path wc\b.txt -Value abc; svn-add wc\b.txt");
                sb.RunScript($@"svn-mkdir wc\x");
                sb.RunScript($@"Set-Content -Path wc\x\c.txt -Value abc; svn-add wc\x\c.txt");
                sb.RunScript($@"svn-commit wc -m test");

                var actual = sb.RunScript($@"svn-export {sb.ReposUrl} export");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"export")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"export\a.txt")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"export\b.txt")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"export\x")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"export\x\c.txt")
                        },
                    },
                    actual);

                Assert.Throws<SvnSystemException>(() => sb.RunScript($@"svn-mkdir wc\a\b\c"));
            }
        }

        [Test]
        public void ExportDirectoryFromWorkingCopy()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript($@"Set-Content -Path wc\a.txt -Value abc; svn-add wc\a.txt");
                sb.RunScript($@"Set-Content -Path wc\b.txt -Value abc; svn-add wc\b.txt");
                sb.RunScript($@"svn-mkdir wc\x");
                sb.RunScript($@"Set-Content -Path wc\x\c.txt -Value abc; svn-add wc\x\c.txt");
                sb.RunScript($@"svn-commit wc -m test");

                var actual = sb.RunScript($@"svn-export {sb.WcPath} export");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"export\a.txt")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"export\b.txt")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"export\x")
                        },
                        new SvnNotifyOutput
                        {
                            Action = SvnNotifyAction.UpdateAdd,
                            Path = Path.Combine(sb.RootPath, @"export\x\c.txt")
                        },
                    },
                    actual);

                Assert.Throws<SvnSystemException>(() => sb.RunScript($@"svn-mkdir wc\a\b\c"));
            }
        }
    }
}
