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

        public DbSet<Parameter> Parameters { get; set; } = default!;

        public DbSet<Permission> Permissions { get; set; } = default!;

        public DbSet<UserPassword> UserPasswords { get; set; } = default!;

        public DbSet<User> Users { get; set; } = default!;

        public DbSet<UserSession> UserSessions { get; set; } = default!;

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder model) =>

            model.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
