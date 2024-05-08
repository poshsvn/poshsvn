// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using SharpSvn.Security;

namespace PoshSvn
{
    [Flags]
    public enum SvnTrustCertificateFailures
    {
        NotYetValid = SvnCertificateTrustFailures.CertificateNotValidYet,
        Expired = SvnCertificateTrustFailures.CertificateExpired,
        CommonNameMismatch = SvnCertificateTrustFailures.CommonNameMismatch,
        UnknownCertificateAuthority = SvnCertificateTrustFailures.UnknownCertificateAuthority,
        Other = SvnCertificateTrustFailures.UnknownSslProviderFailure,
    }
}
