// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Security;
using System.Text;
using PoshSvn.Common;

namespace PoshSvn
{
    public abstract class SvnClientCmdletBase : SvnCmdletBase
    {
        [Parameter(DontShow = true)]
        public SwitchParameter NoAuthCache { get; set; }

        [Parameter(DontShow = true)]
        public string Username { get; set; }

        [Parameter(DontShow = true)]
        public SecureString Password { get; set; }

        [Parameter(DontShow = true)]
        public SvnAccept Accept { get; set; }

        protected SharpSvn.SvnClient SvnClient;

        public SvnClientCmdletBase()
        {
        }

        protected abstract void Execute();

        protected override void BeginProcessing()
        {
            SvnClient = new SharpSvn.SvnClient();
            SvnClient.Notify += NotifyEventHandler;
            SvnClient.Progress += ProgressEventHandler;
            SvnClient.Committed += CommittedEventHandler;
            SvnClient.Conflict += Conflict_Handler;

            if (Username != null || Password != null)
            {
                SvnClient.Authentication.ForceCredentials(Username, Password.AsPlainString());
            }

            SvnClient.Authentication.UserNameHandlers += Authentication_UserNameHandlers;
            SvnClient.Authentication.UserNamePasswordHandlers += Authentication_UserNamePasswordHandlers;
            SvnClient.Authentication.SslServerTrustHandlers += Authentication_SslServerTrustHandlers;
            SvnClient.Authentication.SslServerTrustHandlers += Authentication_SslServerTrustHandlers;
        }

        private void Conflict_Handler(object sender, SharpSvn.SvnConflictEventArgs e)
        {
            SvnConflictSummary conflict = CreateConflict(e);

            conflict.FileName = e.Conflict.Name;
            conflict.Action = e.Conflict.ConflictAction.ToPoshSvnConflictActions();

            WriteObject(conflict);

            if (Accept == SvnAccept.Prompt)
            {
                Collection<ChoiceDescription> choices = new Collection<ChoiceDescription>
                {
                    new ChoiceDescription("&Postpone", "(Postpone) Skip this conflict and leave it unresolved."),
                    new ChoiceDescription("Accept &Base", "(Accept Base) Accept incoming version of entire."),
                    new ChoiceDescription("&Merge", "(Merge) Accept the result file of the automatic merging."),
                    new ChoiceDescription("Accept &Theirs", "(Accept Theirs) Accept incoming version of entire."),
                    new ChoiceDescription("Accept &Mine", "(Accept Mine) Accept local version of entire."),
                };

                int selectedChoice = Host.UI.PromptForChoice(null, string.Format("Merge conflict discovered in file '{0}'", e.Path), choices, 0);

                if (selectedChoice == 0)
                {
                    e.Choice = SharpSvn.SvnAccept.Postpone;
                }
                else if (selectedChoice == 1)
                {
                    e.Choice = SharpSvn.SvnAccept.Base;
                }
                else if (selectedChoice == 2)
                {
                    e.Choice = SharpSvn.SvnAccept.Working;
                }
                else if (selectedChoice == 3)
                {
                    e.Choice = SharpSvn.SvnAccept.Theirs;
                }
                else if (selectedChoice == 4)
                {
                    e.Choice = SharpSvn.SvnAccept.Mine;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                e.Choice = Accept.ToSharpSvnAccept();
            }
        }

        private static SvnConflictSummary CreateConflict(SharpSvn.SvnConflictEventArgs e)
        {
            if (e.ConflictType == SharpSvn.SvnConflictType.Tree)
            {
                return new SvnTreeConflictSummary();
            }
            else if (e.ConflictType == SharpSvn.SvnConflictType.Content)
            {
                return new SvnTextConflictSummary();
            }
            else if (e.ConflictType == SharpSvn.SvnConflictType.Property)
            {
                return new SvnPropertyConflictSummary();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void Authentication_SslServerTrustHandlers(object sender, SharpSvn.Security.SvnSslServerTrustEventArgs e)
        {
            Collection<ChoiceDescription> choices = new Collection<ChoiceDescription>
            {
                new ChoiceDescription("&Reject", "Reject"),
                new ChoiceDescription("Accept &temporarily", "Accept temporarily")
            };
            if (!NoAuthCache)
            {
                choices.Add(new ChoiceDescription("Accept &permanently", "Accept permanently"));
            }

            StringBuilder message = new StringBuilder();
            message.AppendLine(string.Format("Error validating server certificate for '{0}':", e.Realm));

            if (e.Failures.HasFlag(SharpSvn.Security.SvnCertificateTrustFailures.UnknownCertificateAuthority))
            {
                message.AppendLine(" - The certificate is not issued by a trusted authority. Use the fingerprint to validate the certificate manually!");
            }

            if (e.Failures.HasFlag(SharpSvn.Security.SvnCertificateTrustFailures.CommonNameMismatch))
            {
                message.AppendLine(" - The certificate hostname does not match.");
            }

            if (e.Failures.HasFlag(SharpSvn.Security.SvnCertificateTrustFailures.CertificateNotValidYet))
            {
                message.AppendLine(" - The certificate is not yet valid.");
            }

            if (e.Failures.HasFlag(SharpSvn.Security.SvnCertificateTrustFailures.CertificateExpired))
            {
                message.AppendLine(" - The certificate is has expired.");
            }

            if (e.Failures.HasFlag(SharpSvn.Security.SvnCertificateTrustFailures.UnknownSslProviderFailure))
            {
                message.AppendLine(" - The certificate has an unknown error.");
            }

            message.AppendLine("Certificate information:");
            message.AppendLine(string.Format(" - Hostname: {0}", e.CommonName));
            message.AppendLine(string.Format(" - Valid: from {0} until {1}", e.ValidFrom, e.ValidUntil));
            message.AppendLine(string.Format(" - Issuer: {0}", e.Issuer));
            message.AppendLine(string.Format(" - Fingerprint: {0}", e.Fingerprint));

            int selectedChoice = Host.UI.PromptForChoice(null, message.ToString(), choices, 0);
            if (selectedChoice == 0)
            {
                // Reject
                e.Break = true;
            }
            else if (selectedChoice == 1)
            {
                // Accept temporary
                e.AcceptedFailures = e.Failures;
                e.Save = false;
            }
            else if (selectedChoice == 2)
            {
                // Accept permanently
                e.AcceptedFailures = e.Failures;
                e.Save = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Authentication_UserNamePasswordHandlers(object sender, SharpSvn.Security.SvnUserNamePasswordEventArgs e)
        {
            try
            {
                if (e.Realm != null)
                {
                    Host.UI.WriteLine(string.Format("Authentication realm: {0}", e.Realm.ToString()));
                }

                if (e.InitialUserName == null)
                {
                    e.UserName = Host.UI.PromptString("Username");
                }
                else
                {
                    e.UserName = e.InitialUserName;
                }

                using (SecureString password = Host.UI.PromptSecureString(string.Format("Password for '{0}'", e.UserName)))
                {
                    e.Password = password.AsPlainString();
                }

                ApplyAuthPolicy(e);
            }
            catch (OperationCanceledException)
            {
                e.Cancel = true;
            }
            catch (PSInvalidOperationException)
            {
                e.Break = true;
            }
        }

        protected void Authentication_UserNameHandlers(object sender, SharpSvn.Security.SvnUserNameEventArgs e)
        {
            try
            {
                if (e.Realm != null)
                {
                    Host.UI.WriteLine(string.Format("Authentication realm: {0}", e.Realm.ToString()));
                }

                e.UserName = Host.UI.PromptString("Username");

                ApplyAuthPolicy(e);
            }
            catch (OperationCanceledException)
            {
                e.Cancel = true;
            }
            catch (PSInvalidOperationException)
            {
                e.Break = true;
            }
        }

        protected override void ProcessRecord()
        {
            try
            {
                Execute();
            }
            catch (SharpSvn.SvnException ex)
            {
                WriteSvnError(ex);
            }
            catch (Exception ex)
            {
                WriteError(new ErrorRecord(ex, "", ErrorCategory.WriteError, this)
                {
                    ErrorDetails = new ErrorDetails(ex.Message)
                });
            }
        }

        protected void WriteSvnError(SharpSvn.SvnException ex)
        {
            StringBuilder errorDetails = new StringBuilder();

            for (Exception innerException = ex; innerException != null; innerException = innerException.InnerException)
            {
                if (errorDetails.Length > 0)
                {
                    errorDetails.AppendLine();
                }

                errorDetails.Append(innerException.Message);
            }

            ErrorRecord errorRecord = new ErrorRecord(ex, "E" + (int)ex.SvnErrorCode, ErrorCategory.WriteError, this)
            {
                ErrorDetails = new ErrorDetails(errorDetails.ToString()),
            };

            WriteError(errorRecord);
        }

        protected override void EndProcessing()
        {
            SvnClient.Dispose();
        }

        private void ApplyAuthPolicy(SharpSvn.Security.SvnAuthenticationEventArgs args)
        {
            args.Save = !NoAuthCache;
        }
    }
}
