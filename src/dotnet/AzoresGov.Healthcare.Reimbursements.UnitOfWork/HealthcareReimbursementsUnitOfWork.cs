using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public sealed class HealthcareReimbursementsUnitOfWork : EntityFrameworkCoreUnitOfWork, IHealthcareReimbursementsUnitOfWork
    {
        public HealthcareReimbursementsUnitOfWork(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Parameter> Parameters => Set<Parameter>();

        public DbSet<User> Users => Set<User>();

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder model) =>

            model.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
