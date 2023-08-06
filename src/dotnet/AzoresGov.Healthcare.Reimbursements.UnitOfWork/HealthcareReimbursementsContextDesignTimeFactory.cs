using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public sealed class HealthcareReimbursementsContextDesignTimeFactory : IDesignTimeDbContextFactory<HealthcareReimbursementsContext>
    {
        private const string ConnectionString = "Server=127.0.0.1,1433; Database=HealthcareReimbursements; User Id=azoresgov-healthcare-reimbursements-migrator-app; Password=3e0f2c93-0669-4b4b-9c91-4e652ebcd083; Encrypt=False";

        public HealthcareReimbursementsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthcareReimbursementsContext>();

            optionsBuilder.UseSqlServer(ConnectionString, (provider) =>
            {
                provider.EnableRetryOnFailure();
            });

            return new HealthcareReimbursementsContext(optionsBuilder.Options);
        }
    }
}
