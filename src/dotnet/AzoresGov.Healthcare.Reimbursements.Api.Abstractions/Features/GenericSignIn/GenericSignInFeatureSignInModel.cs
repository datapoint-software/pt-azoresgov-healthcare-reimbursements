namespace AzoresGov.Healthcare.Reimbursements.Api.Features.GenericSignIn
{
    public sealed class GenericSignInFeatureSignInModel
    {
        public GenericSignInFeatureSignInModel(string emailAddress, string password, bool persistent)
        {
            EmailAddress = emailAddress;
            Password = password;
            Persistent = persistent;
        }

        public string EmailAddress { get; }

        public string Password { get; }

        public bool Persistent { get; }
    }
}
