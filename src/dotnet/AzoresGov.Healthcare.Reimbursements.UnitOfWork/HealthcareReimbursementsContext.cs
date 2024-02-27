using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public sealed class HealthcareReimbursementsContext : EntityFrameworkCoreContext
    {
        public HealthcareReimbursementsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ParameterEntity> Parameters { get; set; } = default!;

        public DbSet<PermissionEntity> Permissions { get; set; } = default!;

        public DbSet<UserPasswordEntity> UserPasswords { get; set; } = default!;

        public DbSet<UserEntity> Users { get; set; } = default!;

        public DbSet<UserSessionEntity> UserSessions { get; set; } = default!;

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder model) =>

            model.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
