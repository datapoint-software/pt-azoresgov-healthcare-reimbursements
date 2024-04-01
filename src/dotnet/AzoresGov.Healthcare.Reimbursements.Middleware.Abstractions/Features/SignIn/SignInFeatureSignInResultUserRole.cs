using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInFeatureSignInResultUserRole
    {
        public SignInFeatureSignInResultUserRole(Guid id, Guid rowVersionId, UserRoleNature nature)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Nature = nature;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public UserRoleNature Nature { get; }
    }
}
