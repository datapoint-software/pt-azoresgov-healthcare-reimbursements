using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityRefreshResult
    {
        public IdentityRefreshResult(IReadOnlyCollection<IdentityRefreshEntityResult> entities, IReadOnlyCollection<IdentityRefreshPermissionResult> permissions, IdentityRefreshUserResult user, IdentityRefreshUserSessionResult userSession)
        {
            Entities = entities;
            Permissions = permissions;
            User = user;
            UserSession = userSession;
        }

        public IReadOnlyCollection<IdentityRefreshEntityResult> Entities { get; }

        public IReadOnlyCollection<IdentityRefreshPermissionResult> Permissions { get; }

        public IdentityRefreshUserResult User { get; }

        public IdentityRefreshUserSessionResult UserSession { get; }
    }
}