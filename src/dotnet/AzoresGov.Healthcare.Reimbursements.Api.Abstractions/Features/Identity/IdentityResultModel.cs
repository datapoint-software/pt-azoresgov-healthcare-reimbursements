namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    public sealed class IdentityResultModel
    {
        public IdentityResultModel(IdentityUserResultModel user)
        {
            User = user;
        }

        public IdentityUserResultModel User { get; }
    }
}
