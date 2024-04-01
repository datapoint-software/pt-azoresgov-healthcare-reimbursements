using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityFeatureRefreshResult
    {
        public IdentityFeatureRefreshResult(IdentityFeatureRefreshResultUser user, IdentityFeatureRefreshResultUserSession userSession, IReadOnlyCollection<IdentityFeatureRefreshResultUserRole> userRoles)
        {
            User = user;
            UserSession = userSession;
            UserRoles = userRoles;
        }

        public IdentityFeatureRefreshResultUser User { get; }

        public IdentityFeatureRefreshResultUserSession UserSession { get; }

        public IReadOnlyCollection<IdentityFeatureRefreshResultUserRole> UserRoles { get; }
    }
}