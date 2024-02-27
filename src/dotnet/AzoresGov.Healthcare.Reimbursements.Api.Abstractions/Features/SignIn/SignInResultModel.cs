namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInResultModel
    {
        public SignInResultModel(SignInSessionResultModel session, SignInUserResultModel user)
        {
            Session = session;
            User = user;
        }

        public SignInSessionResultModel Session { get; }

        public SignInUserResultModel User { get; }
    }
}
