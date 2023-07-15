using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityRefreshUserSessionResult
    {
        public IdentityRefreshUserSessionResult(Guid id, Guid rowVersionId, DateTimeOffset? expiration)
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