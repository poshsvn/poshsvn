using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class NotifyOutputTests
    {
        [Test]
        public void SimpleTest()
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
    }
}
