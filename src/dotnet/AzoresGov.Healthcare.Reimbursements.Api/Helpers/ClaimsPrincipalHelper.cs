using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AzoresGov.Healthcare.Reimbursements.Api.Helpers
{
    internal static class ClaimsPrincipalHelper
    {
        internal static ClaimsPrincipal CreateClaimsPrincipal(Guid userId, Guid userRowVersionId, Guid userSessionId, Guid userSessionRowVersionId, DateTimeOffset? userSessionExpiration)
        {
            var claims = new List<Claim>()
            {
                { new Claim(ClaimTypes.User, userId.ToString()) },
                { new Claim(ClaimTypes.UserRowVersion, userRowVersionId.ToString()) },
                { new Claim(ClaimTypes.UserSession, userSessionId.ToString()) },
                { new Claim(ClaimTypes.UserSessionRowVersion, userSessionRowVersionId.ToString()) }
            };

            if (userSessionExpiration.HasValue)
                claims.Add(new Claim(ClaimTypes.UserSessionExpiration, userSessionExpiration.Value.ToString()));

            return new ClaimsPrincipal(
                new ClaimsIdentity(
                    claims,
                    "user",
                    ClaimTypes.User,
                    ClaimTypes.Role));
        }

        internal static Guid GetId(this ClaimsPrincipal principal) => principal

            .GetGuid(ClaimTypes.User);

        internal static Guid GetRowVersionId(this ClaimsPrincipal principal) => principal

            .GetGuid(ClaimTypes.UserRowVersion);

        internal static DateTimeOffset? GetSessionExpiration(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserSessionExpiration);

            if (claim == null)
                return null;

            return DateTimeOffset.Parse(claim.Value);
        }

        internal static bool GetSessionPersistent(this ClaimsPrincipal principal) =>

            principal.Claims.Any(c => c.Type == ClaimTypes.UserSessionExpiration);

        internal static Guid GetSessionId(this ClaimsPrincipal principal) => principal

            .GetGuid(ClaimTypes.UserSession);

        internal static Guid GetSessionRowVersionId(this ClaimsPrincipal principal) => principal

            .GetGuid(ClaimTypes.UserSessionRowVersion);

        private static Guid GetGuid(this ClaimsPrincipal principal, string claimType) =>

            Guid.Parse(principal.Claims.First(c => c.Type == claimType).Value);
    }
}
