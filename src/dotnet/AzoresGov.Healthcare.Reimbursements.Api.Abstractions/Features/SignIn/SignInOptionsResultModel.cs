using AzoresGov.Healthcare.Reimbursements.Enumerations;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInOptionsResultModel
    {
        public SignInOptionsResultModel(SignInOptionsMethodsResultModel methods)
        {
            Methods = methods;
        }

        public SignInOptionsMethodsResultModel Methods { get; }
    }
}
