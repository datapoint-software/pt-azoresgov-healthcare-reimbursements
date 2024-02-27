using Microsoft.Extensions.DependencyInjection;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public static class ManagementServiceCollectionExtensions
    {
        public static IServiceCollection AddManagement(this IServiceCollection services) => services

            .AddScoped<IParameterManager, ParameterManager>();
    }
}
