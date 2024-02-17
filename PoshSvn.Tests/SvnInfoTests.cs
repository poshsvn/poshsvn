using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using NUnit.Framework;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnInfoTests
    {
        [Test]
        public void SimpleTest()
        {
            using (WcSandbox sb = new WcSandbox())
            {
                Collection<PSObject> actual = sb.RunScript($"svn-info wc");

                PSObjectAssert.AreEqual(
                    new[]
                    {
                        new SvnInfoLocalOutput
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
                    actual,
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
                        new SvnInfoLocalOutput
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
                        new SvnInfoRemoteOutput
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
    }
}
