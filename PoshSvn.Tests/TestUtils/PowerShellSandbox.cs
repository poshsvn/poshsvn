using System.IO;
using System.Management.Automation;
using System.Reflection;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;
using System.Management.Automation.Runspaces;

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

        public List<string> FormatObject(IEnumerable input, string formatFunction)
        {
            string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            InitialSessionState state = InitialSessionState.CreateDefault();

            state.ImportPSModule(new string[]
            {
                Path.Combine(assemblyDirectory, @"PoshSvn.psd1")
            });

            PowerShell ps = PowerShell.Create(state);

            ps.AddCommand(formatFunction);
            ps.AddCommand("Out-String");
            ps.AddParameter("Stream");
            Collection<PSObject> result = ps.Invoke(input);

            List<string> rv = new List<string>();

            foreach (PSObject obj in result)
            {
                var str = obj.BaseObject.ToString().TrimEnd();
                Console.WriteLine("\"{0}\"", str);
                rv.Add(str);
            }

            return rv;
        }
    }
}
