namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInOptionsMethodsBasicResult
    {
        public SignInOptionsMethodsBasicResult(bool persistentSessionsEnabled)
        {
            PersistentSessionsEnabled = persistentSessionsEnabled;
        }

        public bool PersistentSessionsEnabled { get; }
    }
}