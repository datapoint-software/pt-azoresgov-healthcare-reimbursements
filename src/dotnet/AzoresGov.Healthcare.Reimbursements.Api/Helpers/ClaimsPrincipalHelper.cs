using System;
using System.Linq;
using System.Security.Claims;

namespace AzoresGov.Healthcare.Reimbursements.Api.Helpers
{
    internal static class ClaimsPrincipalHelper
    {
        internal static Guid GetId(this ClaimsPrincipal principal) =>

            Guid.Parse(principal.Claims
                .First(c => c.Type.Equals(
                    ClaimTypes.NameIdentifier,
                    StringComparison.OrdinalIgnoreCase))
                .Value);

        internal static Guid GetSessionId(this ClaimsPrincipal principal) =>

            Guid.Parse(principal.Claims
                .First(c => c.Type.Equals(
                    ClaimTypes.Sid,
                    StringComparison.OrdinalIgnoreCase))
                .Value);
    }
}
