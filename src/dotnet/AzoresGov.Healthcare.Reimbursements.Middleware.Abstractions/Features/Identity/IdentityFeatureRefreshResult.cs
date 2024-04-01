namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityFeatureRefreshResult
    {
        public IdentityFeatureRefreshResult(IdentityFeatureRefreshResultUser user, IdentityFeatureRefreshResultUserSession userSession)
        {
            User = user;
            UserSession = userSession;
        }

        public IdentityFeatureRefreshResultUser User { get; }

        public IdentityFeatureRefreshResultUserSession UserSession { get; }
    }
}