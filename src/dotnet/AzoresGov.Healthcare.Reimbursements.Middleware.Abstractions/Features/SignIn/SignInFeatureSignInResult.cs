using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInFeatureSignInResult
    {
        public SignInFeatureSignInResult(SignInFeatureSignInResultUser user, SignInFeatureSignInResultUserSession userSession, IReadOnlyCollection<SignInFeatureSignInResultUserRole> userRoles)
        {
            User = user;
            UserSession = userSession;
            UserRoles = userRoles;
        }

        public SignInFeatureSignInResultUser User { get; }

        public SignInFeatureSignInResultUserSession UserSession { get; }

        public IReadOnlyCollection<SignInFeatureSignInResultUserRole> UserRoles { get; }
    }
}