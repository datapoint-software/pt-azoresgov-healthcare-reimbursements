namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInOptionsResultModel
    {
        public SignInOptionsResultModel(SignInAuthenticationOptionsResultModel authentication)
        {
            Authentication = authentication;
        }

        public SignInAuthenticationOptionsResultModel Authentication { get; }
    }
}
