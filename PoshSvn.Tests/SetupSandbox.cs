// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    [SetUpFixture]
    public class SetupSandbox
    {
        [OneTimeSetUp]
        public void Cleanup()
        {
            if (Directory.Exists(Sandbox.SandboxRootPath))
            {
                IOUtils.DeleteDir(Sandbox.SandboxRootPath);
            }
        }
    }
}
