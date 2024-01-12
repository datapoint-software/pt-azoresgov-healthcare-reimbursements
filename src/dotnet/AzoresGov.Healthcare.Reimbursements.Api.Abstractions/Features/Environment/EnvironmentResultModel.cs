namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Environment
{
    public sealed class EnvironmentResultModel
    {
        public EnvironmentResultModel(bool production, bool debugSymbols, string fileVersion, string productVersion)
        {
            Production = production;
            DebugSymbols = debugSymbols;
            FileVersion = fileVersion;
            ProductVersion = productVersion;
        }

        public bool Production { get; }

        public bool DebugSymbols { get; }

        public string FileVersion { get; }

        public string ProductVersion { get; }
    }
}
