namespace AzoresGov.Healthcare.Reimbursements.Api.Features.GenericSignIn
{
    public sealed class GenericSignInFeatureOptionsModel
    {
        public GenericSignInFeatureOptionsModel(bool persistentSessionsEnabled)
        {
            PersistentSessionsEnabled = persistentSessionsEnabled;
        }

        public bool PersistentSessionsEnabled { get; }
    }
}
