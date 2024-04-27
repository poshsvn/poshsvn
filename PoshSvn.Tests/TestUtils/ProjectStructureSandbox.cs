// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;

namespace PoshSvn.Tests.TestUtils
{
    public class ProjectStructureSandbox : WcSandbox
    {
        public string TrunkPath { get; }

        public ProjectStructureSandbox() : base()
        {
            TrunkPath = Path.Combine(RootPath, "wc-trunk");

            RunScript($@"svn-mkdir '{ReposUrl}/trunk' '{ReposUrl}/branches' '{ReposUrl}/tags' -m 'Default project structure created.'",
                      $@"svn-checkout '{ReposUrl}/trunk' {TrunkPath}");
        }
    }
}
