using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using NUnit.Framework;
using PoshSvn.Tests.TestUtils;
using DriveNotFoundException = System.Management.Automation.DriveNotFoundException;

namespace PoshSvn.Tests
{
    public class SvnInfoTests
    {
        [Test]
        public void SimpleTest()
        {
            Console.Error.WriteLine(Assembly.GetExecutingAssembly().Location);
            using (WcSandbox sb = new WcSandbox())
            {
                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnInfoOutput
                        {
                            Schedule = SharpSvn.SvnSchedule.Normal,
                            WorkingCopyRoot = sb.WcPath,
                            Path = sb.WcPath,
                            Url = new Uri(sb.ReposUrl + "/"),
                            RelativeUrl = new Uri("", UriKind.Relative),
                            RepositoryRoot = new Uri(sb.ReposUrl + "/"),
                            NodeKind = SharpSvn.SvnNodeKind.Directory,
                            LastChangedAuthor = null,
                        }
                    },
                    sb.RunScript($"svn-info wc"),
                    nameof(SvnInfoOutput.RepositoryId),
                    nameof(SvnInfoOutput.LastChangedDate),
                    nameof(SvnInfoOutput.RepositoryId));

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnInfoOutput
                        {
                            Path = "repos",
                            Url = new Uri(sb.ReposUrl + "/"),
                            RelativeUrl = new Uri("", UriKind.Relative),
                            RepositoryRoot = new Uri(sb.ReposUrl + "/"),
                            NodeKind = SharpSvn.SvnNodeKind.Directory,
                            LastChangedAuthor = null,
                        }
                    },
                    sb.RunScript($"svn-info '{sb.ReposUrl}'"),
                    nameof(SvnInfoOutput.RepositoryId),
                    nameof(SvnInfoOutput.LastChangedDate),
                    nameof(SvnInfoOutput.RepositoryId));
            }
        }

        [Test]
        public void MultiTargetTest()
        {
            using (WcSandbox sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript($"svn-info wc {sb.ReposUrl}");

                PSObjectAssert.AreEqual(
                    new SvnInfoOutput[]
                    {
                        new SvnInfoOutput
                        {
                            Schedule = SharpSvn.SvnSchedule.Normal,
                            WorkingCopyRoot = sb.WcPath,
                            Path = sb.WcPath,
                            Url = new Uri(sb.ReposUrl + "/"),
                            RelativeUrl = new Uri("", UriKind.Relative),
                            RepositoryRoot = new Uri(sb.ReposUrl + "/"),
                            NodeKind = SharpSvn.SvnNodeKind.Directory,
                            LastChangedAuthor = null,
                        },
                        new SvnInfoOutput
                        {
                            Path = "repos",
                            Url = new Uri(sb.ReposUrl + "/"),
                            RelativeUrl = new Uri("", UriKind.Relative),
                            RepositoryRoot = new Uri(sb.ReposUrl + "/"),
                            NodeKind = SharpSvn.SvnNodeKind.Directory,
                            LastChangedAuthor = null,
                        }
                    },
                    actual,
                    nameof(SvnInfoOutput.RepositoryId),
                    nameof(SvnInfoOutput.LastChangedDate),
                    nameof(SvnInfoOutput.RepositoryId));
            }
        }

        [Test]
        public void IncorrectParametersTests()
        {
            using (WcSandbox sb = new WcSandbox())
            {
                Assert.Throws<DriveNotFoundException>(() => sb.RunScript($"svn-info -Path '{sb.ReposUrl}'"));
                Assert.Throws<ArgumentException>(() => sb.RunScript($"svn-info -Url wc"));
            }
        }

        [Test]
        public void CreateDirectoryTest()
        {
            Console.Error.WriteLine(Assembly.GetExecutingAssembly().Location);
            using (WcSandbox sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript(
                    $@"svn-mkdir wc\test",
                    $@"svn-info wc\test");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnInfoOutput
                        {
                            Schedule = SharpSvn.SvnSchedule.Add,
                            WorkingCopyRoot = sb.WcPath,
                            Path = Path.Combine(sb.WcPath, "test"),
                            Url = new Uri(sb.ReposUrl + "/test/"),
                            RelativeUrl = new Uri("test/", UriKind.Relative),
                            RepositoryRoot = new Uri(sb.ReposUrl + "/"),
                            NodeKind = SharpSvn.SvnNodeKind.Directory,
                            LastChangedAuthor = null,
                            Revision = -1,
                            LastChangedRevision = -1,
                        }
                    },
                    actual,
                    nameof(SvnInfoOutput.RepositoryId),
                    nameof(SvnInfoOutput.LastChangedDate),
                    nameof(SvnInfoOutput.RepositoryId));

                actual = sb.RunScript(
                    $@"cd wc",
                    $@"svn-commit -m 'create directory'",
                    $@"svn-info test");

                PSObjectAssert.AreEqual(
                   new[]
                   {
                        new SvnInfoOutput
                        {
                            Schedule = SharpSvn.SvnSchedule.Normal,
                            WorkingCopyRoot = sb.WcPath,
                            Path = Path.Combine(sb.WcPath, "test"),
                            Url = new Uri(sb.ReposUrl + "/test/"),
                            RelativeUrl = new Uri("test/", UriKind.Relative),
                            RepositoryRoot = new Uri(sb.ReposUrl + "/"),
                            NodeKind = SharpSvn.SvnNodeKind.Directory,
                            LastChangedAuthor = null,
                            Revision = 1,
                            LastChangedRevision = 1,
                        }
                   },
                   actual,
                   nameof(SvnInfoOutput.RepositoryId),
                   nameof(SvnInfoOutput.LastChangedDate),
                   nameof(SvnInfoOutput.RepositoryId),
                   nameof(SvnInfoOutput.LastChangedAuthor));

                PSObjectAssert.AreEqual(
                   new[]
                   {
                        new SvnInfoOutput
                        {
                            Path = "repos",
                            Url = new Uri(sb.ReposUrl + "/"),
                            RelativeUrl = new Uri("", UriKind.Relative),
                            RepositoryRoot = new Uri(sb.ReposUrl + "/"),
                            NodeKind = SharpSvn.SvnNodeKind.Directory,
                            Revision = 1,
                            LastChangedRevision = 1,
                        }
                   },
                   sb.RunScript($"svn-info '{sb.ReposUrl}'"),
                   nameof(SvnInfoOutput.RepositoryId),
                   nameof(SvnInfoOutput.LastChangedDate),
                   nameof(SvnInfoOutput.RepositoryId),
                   nameof(SvnInfoOutput.LastChangedAuthor));
            }
        }
    }
}
