using Microsoft.Extensions.Configuration;

namespace AzoresGov.Healthcare.Reimbursements.Api.Helpers
{
    internal static class ConfigurationHelper
    {
        public static int GetAccessTokenExpiration(this IConfiguration configuration) => configuration

                .GetRequiredSection("Authentication")
                .GetRequiredSection("AccessToken")
                .GetValue<int?>("Expiration") ?? 30;
    }
}
