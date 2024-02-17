using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace PoshSvn.Tests
{
    public class SvnInfoTests
    {
        [Test]
        public void SimpleTest()
        {
            using (WcSandbox sb = new WcSandbox())
            {
                Collection<PSObject> actual = PowerShellUtils.RunScript($"svn-info '{sb.WcPath}'");

                ClassicAssert.AreEqual(1, actual.Count);
                var actualRecord = (SvnInfoLocalOutput)actual[0].BaseObject;
                ClassicAssert.AreEqual(sb.WcPath, actualRecord.Path);
                ClassicAssert.AreEqual(sb.WcPath, actualRecord.WorkingCopyRoot);
                ClassicAssert.AreEqual(0, actualRecord.Revision);
                ClassicAssert.AreEqual(sb.ReposUrl + "/", actualRecord.Url.ToString());
            }
        }
    }
}
