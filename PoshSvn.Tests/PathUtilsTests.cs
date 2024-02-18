using System.Reflection;
using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace PoshSvn.Tests
{
    public class PathUtilsTests
    {
        [Test]
        public void GetRelativePathTests()
        {
            Console.Error.WriteLine(Assembly.GetExecutingAssembly().Location);

            ClassicAssert.AreEqual(@"b", PathUtils.GetRelativePath(@"C:\a", @"C:\a\b"));
            ClassicAssert.AreEqual(@"C:\b", PathUtils.GetRelativePath(@"C:\a", @"C:\b"));
            ClassicAssert.AreEqual(@"C:\ab", PathUtils.GetRelativePath(@"C:\a", @"C:\ab"));
            ClassicAssert.AreEqual(@"C:\a\b", PathUtils.GetRelativePath(@"C:\a\b\c", @"C:\a\b"));
            ClassicAssert.AreEqual(@"b\c", PathUtils.GetRelativePath(@"C:\a", @"C:\a\b\c"));
            ClassicAssert.AreEqual(@".", PathUtils.GetRelativePath(@"C:\a\b\c", @"C:\a\b\c"));
        }
    }
}
