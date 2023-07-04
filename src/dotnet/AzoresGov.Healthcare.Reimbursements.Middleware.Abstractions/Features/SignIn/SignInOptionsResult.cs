namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInOptionsResult
    {
        public SignInOptionsResult(SignInAuthenticationOptionsResult authentication)
        {
            Authentication = authentication;
        }

        public SignInAuthenticationOptionsResult Authentication { get; }
    }
}