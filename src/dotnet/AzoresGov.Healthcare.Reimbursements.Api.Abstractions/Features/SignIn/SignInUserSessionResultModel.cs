using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInUserSessionResultModel
    {
        public SignInUserSessionResultModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}