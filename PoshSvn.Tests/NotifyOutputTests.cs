using System.IO;
using System.Management.Automation;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.CmdLets;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class NotifyOutputTests
    {
        [Test]
        public void FormatTableTest()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Add,
                            Path = @"C:\path\to\wc\a"
                        },
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Add,
                            Path = @"C:\path\to\wc\b"
                        },
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Delete,
                            Path = @"C:\path\to\wc\c"
                        },
                    },
                    "Format-Table");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        @"",
                        @"Action  Path",
                        @"------  ----",
                        @"A       C:\path\to\wc\a",
                        @"A       C:\path\to\wc\b",
                        @"D       C:\path\to\wc\c",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void FormatCustomTest()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Add,
                            Path = @"C:\path\to\wc\a"
                        },
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Add,
                            Path = @"C:\path\to\wc\b"
                        },
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Delete,
                            Path = @"C:\path\to\wc\c"
                        },
                    },
                    "Format-Custom");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        @"",
                        @"A       C:\path\to\wc\a",
                        @"A       C:\path\to\wc\b",
                        @"D       C:\path\to\wc\c",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void MixedWithCommitTest()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(
                    new object[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Add,
                            Path = @"C:\path\to\wc\a"
                        },
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Add,
                            Path = @"C:\path\to\wc\b"
                        },
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Delete,
                            Path = @"C:\path\to\wc\c"
                        },
                        new SvnCommitOutput
                        {
                            Revision = 78432
                        }
                    },
                    "Format-Custom");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        @"",
                        @"A       C:\path\to\wc\a",
                        @"A       C:\path\to\wc\b",
                        @"D       C:\path\to\wc\c",
                        @"Committed revision 78432.",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void RelativePathTest()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(
                    new[]
                    {
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, @"path\to\item\a"),
                        },
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Add,
                            Path = Path.Combine(sb.WcPath, @"path\to\item\b"),
                        },
                        new SvnNotifyOutput
                        {
                            Action = SharpSvn.SvnNotifyAction.Delete,
                            Path = Path.Combine(sb.WcPath, @"c"),
                        },
                    },
                    "Format-Table");

                CollectionAssert.AreEqual(
                    new string[]
                    {
                        @"",
                        @"Action  Path",
                        @"------  ----",
                        @"A       wc\path\to\item\a",
                        @"A       wc\path\to\item\b",
                        @"D       wc\c",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void CheckoutOutputFormatTest()
        {
            using (var sb = new WcSandbox())
            {
                CollectionAssert.AreEqual(
                    new[]
                    {
                        @"",
                        @"A       wc\path\to\item\a",
                        @"A       wc\path\to\item\b",
                        @"A       wc\c",
                        @"Checked out revision 36743216.",
                        @"",
                        @"",
                    },
                    sb.FormatObject(
                        new object[]
                        {
                            new SvnNotifyOutput
                            {
                                Action = SharpSvn.SvnNotifyAction.Add,
                                Path = Path.Combine(sb.WcPath, @"path\to\item\a"),
                            },
                            new SvnNotifyOutput
                            {
                                Action = SharpSvn.SvnNotifyAction.Add,
                                Path = Path.Combine(sb.WcPath, @"path\to\item\b"),
                            },
                            new SvnNotifyOutput
                            {
                                Action = SharpSvn.SvnNotifyAction.Add,
                                Path = Path.Combine(sb.WcPath, @"c"),
                            },
                            new SvnCheckoutOutput { Revision = 36743216 },
                        },
                        "Format-Custom"));
            }
        }

        [Test]
        public void UpdateOutputFormatTest()
        {
            using (var sb = new WcSandbox())
            {
                CollectionAssert.AreEqual(
                    new[]
                    {
                        @"",
                        @"A       wc\path\to\item\a",
                        @"A       wc\path\to\item\b",
                        @"D       wc\c",
                        @"Updated to revision 6783213.",
                        @"",
                        @"",
                    },
                    sb.FormatObject(
                        new object[]
                        {
                            new SvnNotifyOutput
                            {
                                Action = SharpSvn.SvnNotifyAction.Add,
                                Path = Path.Combine(sb.WcPath, @"path\to\item\a"),
                            },
                            new SvnNotifyOutput
                            {
                                Action = SharpSvn.SvnNotifyAction.Add,
                                Path = Path.Combine(sb.WcPath, @"path\to\item\b"),
                            },
                            new SvnNotifyOutput
                            {
                                Action = SharpSvn.SvnNotifyAction.Delete,
                                Path = Path.Combine(sb.WcPath, @"c"),
                            },
                            new SvnUpdateOutput { Revision = 6783213 },
                        },
                        "Format-Custom"));
            }
        }

        [Test]
        public void ToStringTest1()
        {
            var item = new SvnNotifyOutput()
            {
                Action = SharpSvn.SvnNotifyAction.Add,
                Path = @"C:\path\to\item"
            };

            ClassicAssert.AreEqual(@"A       C:\path\to\item", item.ToString());
        }

        [Test]
        public void ToStringTest2()
        {
            var item = new SvnNotifyOutput()
            {
                Action = SharpSvn.SvnNotifyAction.Delete,
                Path = @"C:\path\to\item"
            };

            ClassicAssert.AreEqual(@"D       C:\path\to\item", item.ToString());
        }

        [Test]
        public void ToStringTest3()
        {
            var item = new SvnNotifyOutput()
            {
                Action = SharpSvn.SvnNotifyAction.Revert,
                Path = @"C:\path\to\item"
            };

            ClassicAssert.AreEqual(@"Revert  C:\path\to\item", item.ToString());
        }
    }
}
