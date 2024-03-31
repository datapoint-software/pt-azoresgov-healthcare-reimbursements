namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public class SignInFeatureOptions
    {
        public SignInFeatureOptions(bool persistentSessionsEnabled)
        {
            PersistentSessionsEnabled = persistentSessionsEnabled;
        }

        public bool PersistentSessionsEnabled { get; }
    }
}