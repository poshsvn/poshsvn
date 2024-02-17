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
                    nameof(SvnInfoLocalOutput.RepositoryId),
                    nameof(SvnInfoLocalOutput.LastChangedDate),
                    nameof(SvnInfoLocalOutput.RepositoryId));
            }
        }
    }
}
