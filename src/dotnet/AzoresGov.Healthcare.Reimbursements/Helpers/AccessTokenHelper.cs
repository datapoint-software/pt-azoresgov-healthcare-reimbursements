using AzoresGov.Healthcare.Reimbursements.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Cryptography;

namespace AzoresGov.Healthcare.Reimbursements.Helpers
{
    internal static class AccessTokenHelper
    {
        private const string AccessTokenSignatureDigest = "azoresgov-healthcare-reimbursements/3.0.0";

        internal static AccessTokenManager CreateManager(IConfiguration configuration, ILogger logger)
        {
            EnsureSignatureSecurityKeyExists(configuration, logger);

            return new AccessTokenManager(
                configuration.GetBaseAddress(),
                configuration.GetAccessTokenSecurityAlgorithm(),
                AccessTokenSignatureDigest,
                GetSignaturePrivateKey(configuration),
                GetSignaturePublicKey(configuration),
                new JwtSecurityTokenHandler());
        }

        private static void EnsureSignatureSecurityKeyExists(IConfiguration configuration, ILogger logger)
        {
            var signaturePrivateKeyPemFilePath = configuration.GetAccessTokenSignaturePrivateKeyPemFilePath();
            var signaturePublicKeyPemFilePath = configuration.GetAccessTokenSignaturePublicKeyPemFilePath();

            if (!File.Exists(signaturePrivateKeyPemFilePath) || !File.Exists(signaturePublicKeyPemFilePath))
            {
                logger.LogInformation("Generating access token signature keys.");

                var rsa = RSA.Create(4096);

                Directory.CreateDirectory(Path.GetDirectoryName(signaturePrivateKeyPemFilePath)!);
                File.WriteAllText(signaturePrivateKeyPemFilePath, rsa.ExportRSAPrivateKeyPem());

                Directory.CreateDirectory(Path.GetDirectoryName(signaturePublicKeyPemFilePath)!);
                File.WriteAllText(signaturePublicKeyPemFilePath, rsa.ExportRSAPublicKeyPem());
            }
        }

        private static RsaSecurityKey GetSignaturePublicKey(IConfiguration configuration)
        {
            var signaturePublicKeyPemFilePath = configuration.GetAccessTokenSignaturePublicKeyPemFilePath();

            var rsa = RSA.Create();

            rsa.ImportFromPem(File.ReadAllText(signaturePublicKeyPemFilePath));

            return new RsaSecurityKey(rsa);
        }

        private static RsaSecurityKey GetSignaturePrivateKey(IConfiguration configuration)
        {
            var signaturePrivateKeyPemFilePath = configuration.GetAccessTokenSignaturePrivateKeyPemFilePath();

            var rsa = RSA.Create();

            rsa.ImportFromPem(File.ReadAllText(signaturePrivateKeyPemFilePath));

            return new RsaSecurityKey(rsa);
        }
    }
}
