using System.Collections.Generic;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    public class IdentityRefreshEntityResultModel
    {
        public IdentityRefreshEntityResultModel(Guid id, IReadOnlyCollection<IdentityRefreshPermissionResultModel> permissions)
        {
            Id = id;
            Permissions = permissions;
        }

        public Guid Id { get; }

        public IReadOnlyCollection<IdentityRefreshPermissionResultModel> Permissions { get; }
    }
}