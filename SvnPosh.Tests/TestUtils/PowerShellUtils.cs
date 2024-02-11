using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace SvnPosh.Tests.TestUtils
{
    public class PowerShellUtils
    {
        public static Collection<PSObject> RunScript(params string[] commands)
        {
            string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            PowerShell ps = PowerShell.Create();

            ps.AddCommand("ipmo").AddArgument(Path.Combine(assemblyDirectory, @"..\SvnPosh\SvnPosh.psd1"));

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
                throw new Exception($"Error while excecuting command '{commands}'", o.Exception);
            }

            return result;
        }
    }
}
