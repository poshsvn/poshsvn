// Copyright (c) Timofei Zhakov. All rights reserved.

using SharpSvn.Security;

namespace PoshSvn
{
    public static class SvnTrustCertificateFailuresExtensions
    {
        public static SvnCertificateTrustFailures ToSharpSvnTrustCertificateFailures(
            this SvnTrustCertificateFailures failures)
        {
            return (SvnCertificateTrustFailures)failures;
        }
    }
}
