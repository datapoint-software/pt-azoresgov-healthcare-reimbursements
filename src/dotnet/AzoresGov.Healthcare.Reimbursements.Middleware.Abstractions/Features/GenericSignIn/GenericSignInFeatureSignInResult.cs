using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.GenericSignIn
{
    public sealed class GenericSignInFeatureSignInResult
    {
        public GenericSignInFeatureSignInResult(GenericSignInFeatureSignInResultUser user, GenericSignInFeatureSignInResultUserSession userSession, IReadOnlyCollection<GenericSignInFeatureSignInResultUserRole> userRoles)
        {
            User = user;
            UserSession = userSession;
            UserRoles = userRoles;
        }

        public GenericSignInFeatureSignInResultUser User { get; }

        public GenericSignInFeatureSignInResultUserSession UserSession { get; }

        public IReadOnlyCollection<GenericSignInFeatureSignInResultUserRole> UserRoles { get; }
    }
}