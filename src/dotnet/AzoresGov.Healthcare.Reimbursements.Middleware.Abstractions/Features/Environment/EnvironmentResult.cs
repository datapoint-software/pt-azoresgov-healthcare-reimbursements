namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Environment
{
    public sealed class EnvironmentResult
    {
        public EnvironmentResult(string productVersion)
        {
            ProductVersion = productVersion;
        }

        public string ProductVersion { get; }
    }
}
