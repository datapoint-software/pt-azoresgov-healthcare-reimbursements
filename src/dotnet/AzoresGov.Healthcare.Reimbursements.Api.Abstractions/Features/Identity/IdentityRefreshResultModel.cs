using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    public sealed class IdentityRefreshResultModel
    {
        public IdentityRefreshResultModel(
            IReadOnlyCollection<IdentityRefreshEntityResultModel> entities, 
            IReadOnlyCollection<IdentityRefreshPermissionResultModel> permissions, 
            IdentityRefreshUserResultModel user, 
            IdentityRefreshUserSessionResultModel userSession)
        {
            Entities = entities;
            Permissions = permissions;
            User = user;
            UserSession = userSession;
        }

        public IReadOnlyCollection<IdentityRefreshEntityResultModel> Entities { get; }

        public IReadOnlyCollection<IdentityRefreshPermissionResultModel> Permissions { get; }

        public IdentityRefreshUserResultModel User { get; }

        public IdentityRefreshUserSessionResultModel UserSession { get; }
    }
}
