// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.IO;
using NUnit.Framework;

namespace PoshSvn.Tests.TestUtils
{
    public class Sandbox : IDisposable
    {
        public static string SandboxRootPath = Path.Combine(Path.GetTempPath(), "PoshSvnTests");

        public string RootPath { get; }

        public Sandbox()
        {
            RootPath = Path.Combine(SandboxRootPath, TestContext.CurrentContext.Test.ID);
            Directory.CreateDirectory(RootPath);
        }

        public void Dispose()
        {
        }
    }
}
