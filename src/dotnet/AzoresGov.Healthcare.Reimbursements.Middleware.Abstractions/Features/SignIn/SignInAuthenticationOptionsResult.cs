namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInAuthenticationOptionsResult
    {
        public SignInAuthenticationOptionsResult(bool enabled, bool persistentEnabled)
        {
            Enabled = enabled;
            PersistentEnabled = persistentEnabled;
        }

        public bool Enabled { get; }

        public bool PersistentEnabled { get; }
    }
}
