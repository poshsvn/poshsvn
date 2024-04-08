// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Diagnostics;
using NUnit.Framework;

namespace PoshSvn.Tests
{
    [SetUpFixture]
    public class SetupTrace
    {
        [OneTimeSetUp]
        public void StartTest()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            Trace.Flush();
        }
    }
}
