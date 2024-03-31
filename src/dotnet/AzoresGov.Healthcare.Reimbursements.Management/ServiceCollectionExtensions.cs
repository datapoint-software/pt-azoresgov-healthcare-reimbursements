using Microsoft.Extensions.DependencyInjection;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddManagement(this IServiceCollection services) => services
            
            .AddScoped<IParameterManager, ParameterManager>();
    }
}
