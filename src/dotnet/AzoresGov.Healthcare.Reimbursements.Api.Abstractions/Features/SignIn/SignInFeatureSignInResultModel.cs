using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInFeatureSignInResultModel
    {
        public SignInFeatureSignInResultModel(Guid id, Guid rowVersionId, string name, string emailAddress, DateTimeOffset? expiration, IReadOnlyCollection<UserRoleNature> roles)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Name = name;
            EmailAddress = emailAddress;
            Expiration = expiration;
            Roles = roles;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public string Name { get; }

        public string EmailAddress { get; }

        public DateTimeOffset? Expiration { get; }

        public IReadOnlyCollection<UserRoleNature> Roles { get; }
    }
}
