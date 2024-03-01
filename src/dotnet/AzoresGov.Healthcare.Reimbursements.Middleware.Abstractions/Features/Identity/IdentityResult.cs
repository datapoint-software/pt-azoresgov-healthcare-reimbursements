using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityResult
    {
        public IdentityResult(IReadOnlyCollection<IdentityRoleResult> roles, IdentityUserResult user)
        {
            Roles = roles;
            User = user;
        }

        public IReadOnlyCollection<IdentityRoleResult> Roles { get; }

        public IdentityUserResult User { get; }
    }
}