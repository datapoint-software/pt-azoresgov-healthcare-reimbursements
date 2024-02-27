using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInSessionResultModel
    {
        public SignInSessionResultModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
