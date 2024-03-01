using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityRoleResult
    {
        public IdentityRoleResult(Guid id, Guid rowVersionId, string name)
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