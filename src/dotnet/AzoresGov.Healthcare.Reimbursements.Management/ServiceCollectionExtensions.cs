using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddManagement(this IServiceCollection services) => services
            
            .AddScoped<IEntityManager, EntityManager>()
            .AddScoped<IParameterManager, ParameterManager>();
    }
}
