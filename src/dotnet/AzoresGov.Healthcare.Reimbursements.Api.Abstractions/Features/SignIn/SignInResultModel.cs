using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInResultModel
    {
        public SignInResultModel(string accessToken, int accessTokenExpiration, string refreshToken)
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
