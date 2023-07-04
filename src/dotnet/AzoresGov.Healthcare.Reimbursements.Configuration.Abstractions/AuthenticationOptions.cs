namespace AzoresGov.Healthcare.Reimbursements.Configuration
{
    public sealed class AuthenticationOptions
    {
        public AuthenticationOptions(bool enabled)
        {
            Enabled = enabled;
        }

        public bool Enabled { get; }
    }
}
