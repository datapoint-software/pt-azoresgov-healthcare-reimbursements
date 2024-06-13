namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.GenericSignIn
{
    public class GenericSignInFeatureOptions
    {
        public GenericSignInFeatureOptions(bool persistentSessionsEnabled)
        {
            PersistentSessionsEnabled = persistentSessionsEnabled;
        }

        public bool PersistentSessionsEnabled { get; }
    }
}