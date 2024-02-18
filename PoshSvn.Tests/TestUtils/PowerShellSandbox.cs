using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System;

namespace PoshSvn.Tests.TestUtils
{
    public class PowerShellSandbox : Sandbox
    {
        public Collection<PSObject> RunScript(params string[] commands)
        {
            string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            PowerShell ps = PowerShell.Create();

            ps.AddCommand($"ipmo")
              .AddArgument(Path.Combine(assemblyDirectory, @"PoshSvn.psd1"))
              .Invoke();
            ps.Commands.Clear();

            ps.AddCommand("cd")
              .AddArgument(RootPath)
              .Invoke();
            ps.Commands.Clear();

            foreach (string command in commands)
            {
                ps.AddScript(command);
            }

            var result = ps.Invoke();

            foreach (var o in ps.Streams.Warning)
            {
                Console.WriteLine(o);
            }

            foreach (var o in ps.Streams.Error)
            {
                throw o.Exception;
            }

            return result;
        }
    }
}
