// Copyright (c) Timofei Zhakov. All rights reserved.

using NUnit.Framework;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class NewSvnTargetTests
    {
        [Test]
        public void ParseFromAbsolutePath()
        {
            using (var sb = new PowerShellSandbox())
            {
                PSObjectAssert.AreEqual(
                    new[]
                    {
                        PoshSvnTarget.FromPath(sb.RootPath)
                    },
                    sb.RunScript($@"New-SvnTarget '{sb.RootPath}'"));
            }
        }

        [Test]
        public void ParseFromAbsoluteUrl()
        {
            using (var sb = new PowerShellSandbox())
            {
                PSObjectAssert.AreEqual(
                    new[]
                    {
                        PoshSvnTarget.FromUrl("http://svn.example.com/repos/test/foo.c")
                    },
                    sb.RunScript($@"New-SvnTarget 'http://svn.example.com/repos/test/foo.c'"));
            }
        }
    }
}
