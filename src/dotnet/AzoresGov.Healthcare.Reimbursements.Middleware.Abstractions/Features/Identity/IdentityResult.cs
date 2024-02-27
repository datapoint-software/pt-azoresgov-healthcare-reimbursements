namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityResult
    {
        public IdentityResult(IdentityUserResult user)
        {
            User = user;
        }

        public IdentityUserResult User { get; }
    }
}