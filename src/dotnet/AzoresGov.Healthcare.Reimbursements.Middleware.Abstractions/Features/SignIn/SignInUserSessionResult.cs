using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInUserSessionResult
    {
        public SignInUserSessionResult(Guid id, Guid rowVersionId, DateTimeOffset? expiration)
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