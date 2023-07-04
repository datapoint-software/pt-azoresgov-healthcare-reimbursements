namespace AzoresGov.Healthcare.Reimbursements.Configuration
{
    public sealed class UserPasswordHashOptions
    {
        public UserPasswordHashOptions(int workFactor)
        {
            WorkFactor = workFactor;
        }

        public int WorkFactor { get; }
    }
}
