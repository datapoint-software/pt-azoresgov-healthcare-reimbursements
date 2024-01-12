namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Environment
{
    public sealed class EnvironmentResult
    {
        public EnvironmentResult(bool debugSymbols, string fileVersion, string productVersion)
        {
            DebugSymbols = debugSymbols;
            FileVersion = fileVersion;
            ProductVersion = productVersion;
        }

        public bool DebugSymbols { get; }

        public string FileVersion { get; }

        public string ProductVersion { get; }
    }
}
