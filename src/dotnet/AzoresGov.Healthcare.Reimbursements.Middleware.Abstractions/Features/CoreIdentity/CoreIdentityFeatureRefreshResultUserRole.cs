using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.CoreIdentity
{
    public sealed class CoreIdentityFeatureRefreshResultUserRole
    {
        public CoreIdentityFeatureRefreshResultUserRole(Guid id, Guid rowVersionId, UserRoleNature nature)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Nature = nature;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public UserRoleNature Nature { get; }
    }
}