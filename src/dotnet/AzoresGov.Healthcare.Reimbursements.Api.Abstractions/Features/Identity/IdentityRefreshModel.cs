namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    public sealed class IdentityRefreshModel
    {
        public IdentityRefreshModel(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public string RefreshToken { get; }
    }
}
