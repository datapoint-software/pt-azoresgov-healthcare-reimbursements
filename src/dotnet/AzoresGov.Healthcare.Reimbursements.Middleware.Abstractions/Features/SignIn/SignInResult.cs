using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInResult
    {
        public SignInResult(IReadOnlyCollection<SignInEntityResult> entities, IReadOnlyCollection<SignInPermissionResult> permissions, SignInUserResult user, SignInUserSessionResult userSession)
        {
            Entities = entities;
            Permissions = permissions;
            User = user;
            UserSession = userSession;
        }

        public IReadOnlyCollection<SignInEntityResult> Entities { get; }

        public IReadOnlyCollection<SignInPermissionResult> Permissions { get; }

        public SignInUserResult User { get; }

        public SignInUserSessionResult UserSession { get; }
    }
}