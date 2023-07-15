using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInEntityResult
    {
        public SignInEntityResult(Guid id, IReadOnlyCollection<SignInPermissionResult> permissions)
        {
            Id = id;
            Permissions = permissions;
        }

        public Guid Id { get; }

        public IReadOnlyCollection<SignInPermissionResult> Permissions { get; }
    }
}