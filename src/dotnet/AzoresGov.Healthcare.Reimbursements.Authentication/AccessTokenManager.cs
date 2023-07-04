using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AzoresGov.Healthcare.Reimbursements.Authentication
{
    public sealed class AccessTokenManager : IAccessTokenManager
    {
        public AccessTokenManager(
            string name, 
            string signatureAlgorithm, 
            string signatureDigest, 
            RsaSecurityKey signatureKey, 
            RsaSecurityKey signaturePublicKey, 
            JwtSecurityTokenHandler securityTokenHandler)
        {
            Name = name;
            SignatureAlgorithm = signatureAlgorithm;
            SignatureDigest = signatureDigest;
            SignatureKey = signatureKey;
            SignaturePublicKey = signaturePublicKey;
            SecurityTokenHandler = securityTokenHandler;
        }

        public string Name { get; }

        public string SignatureAlgorithm { get; }

        public string SignatureDigest { get; }

        public RsaSecurityKey SignatureKey { get; }

        public RsaSecurityKey SignaturePublicKey { get; }

        public JwtSecurityTokenHandler SecurityTokenHandler { get; }

        public string CreateAccessToken(ClaimsPrincipal principal, DateTimeOffset issued, int expiration)
        {
            var token = SecurityTokenHandler.CreateToken(
                new SecurityTokenDescriptor()
                {
                    Audience = Name,
                    Issuer = Name,
                    IssuedAt = issued.UtcDateTime,
                    Expires = issued.UtcDateTime.AddSeconds(expiration),
                    SigningCredentials = new SigningCredentials(SignatureKey, SignatureAlgorithm),
                    Subject = new ClaimsIdentity(principal.Identity)
                });

            return SecurityTokenHandler.WriteToken(token);
        }
    }
}
