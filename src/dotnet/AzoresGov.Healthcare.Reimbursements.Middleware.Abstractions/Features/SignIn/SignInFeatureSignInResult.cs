using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInFeatureSignInResult
    {
        public SignInFeatureSignInResult(SignInFeatureSignInResultUser user, SignInFeatureSignInResultUserSession userSession)
        {
            User = user;
            UserSession = userSession;
        }

        public SignInFeatureSignInResultUser User { get; }

        public SignInFeatureSignInResultUserSession UserSession { get; }
    }
}