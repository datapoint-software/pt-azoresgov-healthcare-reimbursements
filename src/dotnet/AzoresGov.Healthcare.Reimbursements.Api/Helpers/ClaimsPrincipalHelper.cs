using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AzoresGov.Healthcare.Reimbursements.Api.Helpers
{
    internal static class ClaimsPrincipalHelper
    {
        internal static ClaimsPrincipal CreateUserClaimsPrincipal(
            string authenticationType,
            Guid userId,
            Guid userSessionId,
            string userName,
            string userEmailAddress,
            IReadOnlyCollection<UserRoleNature> userRoleNatures) =>

            new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new (ClaimTypes.Sid, userSessionId.ToString()),
                        new (ClaimTypes.NameIdentifier, userId.ToString()),
                        new (ClaimTypes.Name, userName),
                        new (ClaimTypes.Email, userEmailAddress),
                    }
                        .Union(userRoleNatures.Select((urn) => new Claim(ClaimTypes.Role, urn.ToString())))
                        .ToArray(),
                    authenticationType,
                    ClaimTypes.Name,
                    ClaimTypes.Role));

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
