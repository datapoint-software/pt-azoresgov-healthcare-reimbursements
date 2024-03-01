using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInResult
    {
        public SignInResult(IReadOnlyCollection<SignInRoleResult> roles, SignInSessionResult session, SignInUserResult user)
        {
            Roles = roles;
            Session = session;
            User = user;
        }

        public IReadOnlyCollection<SignInRoleResult> Roles { get; }

        public SignInSessionResult Session { get; }

        public SignInUserResult User { get; }
    }
}
