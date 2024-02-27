using AzoresGov.Healthcare.Reimbursements.Enumerations;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Environment
{
    public sealed class EnvironmentResultModel
    {
        public EnvironmentResultModel(EnvironmentNature nature, string productVersion)
        {
            Nature = nature;
            ProductVersion = productVersion;
        }

        public EnvironmentNature Nature { get; }

        public string ProductVersion { get; }
    }
}
