using AzoresGov.Healthcare.Reimbursements.Configuration;
using Datapoint.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class ConfigurationHelper
    {
        internal static Task<UserPasswordHashOptions> GetUserPasswordHashOptionsAsync(this IConfiguration configuration, CancellationToken ct) =>

            configuration.GetOptionsAsync<UserPasswordHashOptions>(ct);
    }
}
