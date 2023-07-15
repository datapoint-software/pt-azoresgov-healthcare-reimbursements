using System.Collections.Generic;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public class IdentityRefreshEntityResult
    {
        public IdentityRefreshEntityResult(Guid id, IReadOnlyCollection<IdentityRefreshPermissionResult> permissions)
        {
            Id = id;
            Permissions = permissions;
        }

        public Guid Id { get; }

        public IReadOnlyCollection<IdentityRefreshPermissionResult> Permissions { get; }
    }
}