using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInUserResultModel
    {
        public SignInUserResultModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}