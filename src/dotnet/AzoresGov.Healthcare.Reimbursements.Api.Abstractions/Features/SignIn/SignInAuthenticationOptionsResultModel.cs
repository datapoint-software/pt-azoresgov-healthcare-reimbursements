namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInAuthenticationOptionsResultModel
    {
        public SignInAuthenticationOptionsResultModel(bool enabled, bool persistentEnabled)
        {
            Enabled = enabled;
            PersistentEnabled = persistentEnabled;
        }

        public bool Enabled { get; }

        public bool PersistentEnabled { get; }
    }
}