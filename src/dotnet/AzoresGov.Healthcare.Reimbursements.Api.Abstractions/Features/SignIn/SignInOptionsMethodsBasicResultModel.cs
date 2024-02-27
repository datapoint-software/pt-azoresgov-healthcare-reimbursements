namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public class SignInOptionsMethodsBasicResultModel
    {
        public SignInOptionsMethodsBasicResultModel(bool persistentSessionsEnabled)
        {
            PersistentSessionsEnabled = persistentSessionsEnabled;
        }

        public bool PersistentSessionsEnabled { get; }
    }
}