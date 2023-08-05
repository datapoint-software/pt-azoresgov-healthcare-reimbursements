using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork
{
    public sealed class HealthcareReimbursementsContext : DbContext
    {
        public HealthcareReimbursementsContext(DbContextOptions<HealthcareReimbursementsContext> options) : base(options)
        {
            ChangeTracker.Tracked += (sender, e) =>
            {
                if (e.Entry.State == EntityState.Added)
                {
                    if (e.Entry.Entity is IEntity entity)
                    {
                        if (entity.PublicId == default)
                            entity.PublicId = Guid.NewGuid();

                        if (entity.RowVersionId == default)
                            entity.RowVersionId = Guid.NewGuid();
                    }
                }
            };

            SavingChanges += (sender, e) =>
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    if (entry.State == EntityState.Modified)
                    {
                        if (entry.Entity is IEntity entity)
                        {
                            if (entity.PublicId == default)
                                entity.PublicId = Guid.NewGuid();

                            else if (entity.RowVersionId == default || entity.RowVersionId == entry.OriginalValues.GetValue<Guid>("RowVersionId"))
                                entity.RowVersionId = Guid.NewGuid();
                        }
                    }
                }
            };

            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder model) =>

            model.ApplyConfigurationsFromAssembly(typeof(HealthcareReimbursementsContext).Assembly);

        public DbSet<EntityEntity> Entities { get; set; } = default!;

        public DbSet<EntityParentEntity> EntityParents { get; set; } = default!;

        public DbSet<ParameterEntity> Parameters { get; set; } = default!;

        public DbSet<PermissionEntity> Permissions { get; set; } = default!;

        public DbSet<RolePermissionEntity> RolePermissions { get; set; } = default!;

        public DbSet<RoleEntity> Roles { get; set; } = default!;

        public DbSet<UserAgentEntity> UserAgents { get; set; } = default!;

        public DbSet<UserEmailAddressEntity> UserEmailAddresses { get; set; } = default!;

        public DbSet<UserEntityEntity> UserEntities { get; set; } = default!;

        public DbSet<UserEntityPermissionEntity> UserEntityPermissions { get; set; } = default!;

        public DbSet<UserEntityRoleEntity> UserEntityRoles { get; set; } = default!;

        public DbSet<UserPasswordEntity> UserPasswords { get; set; } = default!;

        public DbSet<UserPermissionEntity> UserPermissions { get; set; } = default!;

        public DbSet<UserRoleEntity> UserRoles { get; set; } = default!;

        public DbSet<UserEntity> Users { get; set; } = default!;

        public DbSet<UserSessionEntity> UserSessions { get; set; } = default!;
    }
}