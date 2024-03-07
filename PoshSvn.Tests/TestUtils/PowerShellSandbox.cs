using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;

namespace PoshSvn.Tests.TestUtils
{
    public class PowerShellSandbox : Sandbox
    {
        static readonly InitialSessionState initialState = CreateInitialState();

        public PowerShellSandbox()
        {
        }

        public Collection<PSObject> RunScript(params string[] commands)
        {
            PowerShell ps = CreatePowerShell();

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
            PowerShell ps = CreatePowerShell();

            ps.AddCommand("cd");
            ps.AddArgument(RootPath);
            ps.Invoke();
            ps.Commands.Clear();

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

        private PowerShell CreatePowerShell()
        {
            return PowerShell.Create(initialState);
        }

        private static InitialSessionState CreateInitialState()
        {
            string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var state = InitialSessionState.CreateDefault();

            state.ImportPSModule(new string[]
            {
                Path.Combine(assemblyDirectory, @"PoshSvn.psd1")
            });

            return state;
        }
    }
}
