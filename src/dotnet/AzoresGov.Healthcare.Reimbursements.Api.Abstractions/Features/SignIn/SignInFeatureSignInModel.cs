namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInFeatureSignInModel
    {
        public SignInFeatureSignInModel(string emailAddress, string password, bool persistent)
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
