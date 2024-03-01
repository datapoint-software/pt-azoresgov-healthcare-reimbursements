using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInRoleResultModel
    {
        public SignInRoleResultModel(Guid id, Guid rowVersionId, string name)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Name = name;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public string Name { get; }
    }
}