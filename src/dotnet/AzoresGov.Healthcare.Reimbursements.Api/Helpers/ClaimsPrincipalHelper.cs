using System;
using System.Linq;
using System.Security.Claims;

namespace AzoresGov.Healthcare.Reimbursements.Api.Helpers
{
    internal static class ClaimsPrincipalHelper
    {
        internal static ClaimsPrincipal CreateClaimsPrincipal(Guid userId, Guid userRowVersionId, Guid userSessionId, Guid userSessionRowVersionId) =>

            new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] 
                    { 
                        new Claim(ClaimTypes.User, userId.ToString()),
                        new Claim(ClaimTypes.UserRowVersion, userRowVersionId.ToString()),
                        new Claim(ClaimTypes.UserSession, userSessionId.ToString()),
                        new Claim(ClaimTypes.UserSessionRowVersion, userSessionRowVersionId.ToString())
                    },
                    "user",
                    ClaimTypes.User,
                    ClaimTypes.Role));

        internal static Guid GetId(this ClaimsPrincipal principal) => principal

            .GetGuid(ClaimTypes.User);

        internal static Guid GetRowVersionId(this ClaimsPrincipal principal) => principal

            .GetGuid(ClaimTypes.UserRowVersion);

        internal static Guid GetSessionId(this ClaimsPrincipal principal) => principal

            .GetGuid(ClaimTypes.UserSession);

        internal static Guid GetSessionRowVersionId(this ClaimsPrincipal principal) => principal

            .GetGuid(ClaimTypes.UserSessionRowVersion);

        private static Guid GetGuid(this ClaimsPrincipal principal, string claimType) =>

            Guid.Parse(principal.Claims.First(c => c.Type == claimType).Value);
    }
}
