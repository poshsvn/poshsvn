using System;
using System.Management.Automation.Host;
using System.Security;

namespace PoshSvn
{
    public static class PSHostUserInterfaceExtensions
    {
        public static string PromptString(this PSHostUserInterface ui, string text)
        {
            ui.Write(text + ": ");
            string result = ui.ReadLine();

            if (result == null)
            {
                throw new OperationCanceledException();
            }

            return result;
        }

        public static SecureString PromptSecureString(this PSHostUserInterface ui, string text)
        {
            ui.Write(text + ": ");

            var result = ui.ReadLineAsSecureString();
            if (result == null)
            {
                throw new OperationCanceledException();
            }

            return result;
        }
    }
}
