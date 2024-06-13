using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.CoreIdentity
{
    public sealed class CoreIdentityFeatureRefreshResult
    {
        public CoreIdentityFeatureRefreshResult(CoreIdentityFeatureRefreshResultUser user, CoreIdentityFeatureRefreshResultUserSession userSession, IReadOnlyCollection<CoreIdentityFeatureRefreshResultUserRole> userRoles)
        {
            User = user;
            UserSession = userSession;
            UserRoles = userRoles;
        }

        public CoreIdentityFeatureRefreshResultUser User { get; }

        public CoreIdentityFeatureRefreshResultUserSession UserSession { get; }

        public IReadOnlyCollection<CoreIdentityFeatureRefreshResultUserRole> UserRoles { get; }
    }
}