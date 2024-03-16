// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.IO;

namespace PoshSvn.Tests.TestUtils
{
    public class Sandbox : IDisposable
    {
        public string RootPath { get; }

        public Sandbox()
        {
            var rootPath = Path.Combine(Path.GetTempPath(), "PoshSvnTests");
            if (Directory.Exists(rootPath))
            {
                IOUtils.DeleteDir(rootPath);
            }

            Directory.CreateDirectory(rootPath);

            RootPath = rootPath;
        }

        public void Dispose()
        {
        }
    }
}
