using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInSessionResult
    {
        public SignInSessionResult(Guid id, DateTimeOffset? expiration)
        {
            Id = id;
            Expiration = expiration;
        }

        public Guid Id { get; }

        public DateTimeOffset? Expiration { get; }
    }
}
