namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    public sealed class IdentityRefreshResultModel
    {
        public IdentityRefreshResultModel(string accessToken, int accessTokenExpiration, string refreshToken)
        {
            AccessToken = accessToken;
            AccessTokenExpiration = accessTokenExpiration;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; }

        public int AccessTokenExpiration { get; }

        public string RefreshToken { get; }
    }
}
