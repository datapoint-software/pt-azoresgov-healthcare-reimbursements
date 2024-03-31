using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkCoreUnitOfWork<IHealthcareReimbursementsUnitOfWork, HealthcareReimbursementsUnitOfWork>((unitOfWork) =>
            {
                unitOfWork.UseContextConfiguration((context) =>
                {
                    context.UseMySQL(connectionString, (provider) =>
                    {
                        provider.EnableRetryOnFailure();
                    });
                });

                unitOfWork.AddRepositoriesFromAssemblyOf<HealthcareReimbursementsUnitOfWork>();
                unitOfWork.AddWorkersFromAssemblyOf<HealthcareReimbursementsUnitOfWork>();
            });

            return services;
        }
    }
}
