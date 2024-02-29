using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public static class HealthcareReimbursementsUnitOfWorkServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkCoreUnitOfWork<IHealthcareReimbursementsUnitOfWork, HealthcareReimbursementsUnitOfWork>((unitOfWork) =>
            {
                unitOfWork.UseContextConfiguration((context) =>
                {
                    context.UseSqlServer(connectionString, (provider) =>
                    {
                        provider.EnableRetryOnFailure();
                        provider.UseCompatibilityLevel(160);
                    });
                });

                unitOfWork.AddRepositoriesFromAssemblyOf<HealthcareReimbursementsUnitOfWork>();
                unitOfWork.AddWorkersFromAssemblyOf<HealthcareReimbursementsUnitOfWork>();
            });

            return services;
        }
    }
}
