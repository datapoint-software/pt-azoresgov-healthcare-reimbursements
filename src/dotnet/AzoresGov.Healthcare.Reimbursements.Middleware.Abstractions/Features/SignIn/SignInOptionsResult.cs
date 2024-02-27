using AzoresGov.Healthcare.Reimbursements.Enumerations;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInOptionsResult
    {
        public SignInOptionsResult(SignInOptionsMethodsResult methods)
        {
            Methods = methods;
        }

        public SignInOptionsMethodsResult Methods { get; }
    }
}
