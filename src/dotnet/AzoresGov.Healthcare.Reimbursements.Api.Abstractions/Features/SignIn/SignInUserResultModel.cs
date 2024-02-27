using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInUserResultModel
    {
        public SignInUserResultModel(Guid id, string name, string emailAddress)
        {
            Id = id;
            Name = name;
            EmailAddress = emailAddress;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string EmailAddress { get; }
    }
}
