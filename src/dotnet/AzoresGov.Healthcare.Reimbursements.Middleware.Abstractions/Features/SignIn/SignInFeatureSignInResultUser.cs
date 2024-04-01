using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInFeatureSignInResultUser
    {
        public SignInFeatureSignInResultUser(Guid id, Guid rowVersionId, string name, string emailAddress)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Name = name;
            EmailAddress = emailAddress;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public string Name { get; }

        public string EmailAddress { get; }
    }
}