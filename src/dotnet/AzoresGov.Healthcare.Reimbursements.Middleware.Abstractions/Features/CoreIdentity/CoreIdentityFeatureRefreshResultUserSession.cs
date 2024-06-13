using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.CoreIdentity
{
    public sealed class CoreIdentityFeatureRefreshResultUserSession
    {
        public CoreIdentityFeatureRefreshResultUserSession(Guid id, Guid rowVersionId, DateTimeOffset? expiration)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Expiration = expiration;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public DateTimeOffset? Expiration { get; }
    }
}