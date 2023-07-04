using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AzoresGov.Healthcare.Reimbursements.Helpers
{
    internal static class ConfigurationHelper
    {
        internal static string GetBaseAddress(this IConfiguration configuration)
        {
            var baseAddress = configuration.GetValue<string>("BaseAddress");

            if (string.IsNullOrEmpty(baseAddress))
                throw new InvalidOperationException("Application base address, on property 'BaseAddress', is not set.");

            return baseAddress;
        }

        internal static string GetAccessTokenSecurityAlgorithm(this IConfiguration configuration)
        {
            var securityAlgorithm = configuration
                .GetRequiredSection("Authentication")
                .GetRequiredSection("AccessToken")
                .GetValue<string>("SecurityAlgorithm");

            if (string.IsNullOrEmpty(securityAlgorithm))
                securityAlgorithm = "RS256";

            return securityAlgorithm;
        }

        internal static string GetAccessTokenSignaturePublicKeyPemFilePath(this IConfiguration configuration)
        {
            var signaturePublicKeyPemFilePath = configuration
                .GetRequiredSection("Authentication")
                .GetRequiredSection("AccessToken")
                .GetSection("SecurityKeys")
                .GetSection("Signature")
                .GetValue<string?>("Public")?
                .Replace('/', Path.DirectorySeparatorChar);

            signaturePublicKeyPemFilePath ??= Path.Join(
                "SecurityKeys",
                "AccessToken",
                "Signature",
                "AccessTokenSignature.pem.public");

            if (!Path.IsPathFullyQualified(signaturePublicKeyPemFilePath))
                signaturePublicKeyPemFilePath = Path.Join(
                    AppDomain.CurrentDomain.BaseDirectory,
                    signaturePublicKeyPemFilePath);

            return signaturePublicKeyPemFilePath;
        }

        internal static string GetAccessTokenSignaturePrivateKeyPemFilePath(this IConfiguration configuration)
        {
            var signaturePrivateKeyPemFilePath = configuration
                .GetRequiredSection("Authentication")
                .GetRequiredSection("AccessToken")
                .GetSection("SecurityKeys")
                .GetSection("Signature")
                .GetValue<string?>("Private")?
                .Replace('/', Path.DirectorySeparatorChar);

            signaturePrivateKeyPemFilePath ??= Path.Join(
                "SecurityKeys",
                "AccessToken",
                "Signature",
                "AccessTokenSignature.pem");

            if (!Path.IsPathFullyQualified(signaturePrivateKeyPemFilePath))
                signaturePrivateKeyPemFilePath = Path.Join(
                    AppDomain.CurrentDomain.BaseDirectory,
                    signaturePrivateKeyPemFilePath);

            return signaturePrivateKeyPemFilePath;
        }
    }
}
