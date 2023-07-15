using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityRefreshPermissionResult
    {
        public IdentityRefreshPermissionResult(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}