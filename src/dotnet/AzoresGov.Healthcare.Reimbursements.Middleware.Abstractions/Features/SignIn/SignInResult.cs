namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInResult
    {
        public SignInResult(SignInSessionResult session, SignInUserResult user)
        {
            Session = session;
            User = user;
        }

        public SignInSessionResult Session { get; }

        public SignInUserResult User { get; }
    }
}
