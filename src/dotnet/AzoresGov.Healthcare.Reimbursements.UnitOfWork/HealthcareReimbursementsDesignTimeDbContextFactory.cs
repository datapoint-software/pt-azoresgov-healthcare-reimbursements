using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public sealed class HealthcareReimbursementsDesignTimeDbContextFactory : IDesignTimeDbContextFactory<HealthcareReimbursementsUnitOfWork>
    {
        private const string ConnectionString = "Server=127.0.0.1; Port=3306; Database=Reimbursements; Uid=reimbursements-migrator-app; Pwd=667e9bd5-ffc1-4232-85ae-d085061668b4";

        public HealthcareReimbursementsUnitOfWork CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HealthcareReimbursementsUnitOfWork>();

            optionsBuilder.UseMySQL(ConnectionString, (provider) =>
            {
                provider.EnableRetryOnFailure();
            });

            return new HealthcareReimbursementsUnitOfWork(optionsBuilder.Options);
        }
    }
}
