namespace AzoresGov.Healthcare.Reimbursements.Configuration
{
    public sealed class UserSessionOptions
    {
        public UserSessionOptions(int? expiration)
        {
            Expiration = expiration;
        }

        public int? Expiration { get; }
    }
}
