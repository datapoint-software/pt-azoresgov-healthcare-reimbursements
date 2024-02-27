namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInOptionsMethodsResultModel
    {
        public SignInOptionsMethodsResultModel(SignInOptionsMethodsBasicResultModel? basic)
        {
            Basic = basic;
        }

        public SignInOptionsMethodsBasicResultModel? Basic { get; }
    }
}
