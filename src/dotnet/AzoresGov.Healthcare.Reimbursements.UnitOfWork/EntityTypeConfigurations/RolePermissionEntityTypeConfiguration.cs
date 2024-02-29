using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class RolePermissionEntityTypeConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.Entity();

            builder.HasIndex([ nameof(RolePermission.RoleId), nameof(RolePermission.PermissionId) ])
                .IsUnique();

            builder.HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            builder.HasOne(e => e.Permission)
                .WithMany()
                .HasForeignKey(e => e.PermissionId)
                .IsRequired();

            builder.Property(e => e.Granted)
                .IsRequired();
        }
    }
}