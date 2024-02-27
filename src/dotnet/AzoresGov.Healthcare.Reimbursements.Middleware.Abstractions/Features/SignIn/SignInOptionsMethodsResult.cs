namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInOptionsMethodsResult
    {
        public SignInOptionsMethodsResult(SignInOptionsMethodsBasicResult? basic)
        {
            Basic = basic;
        }

        public SignInOptionsMethodsBasicResult? Basic { get; }
    }
}
