using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class UserPermissionEntityTypeConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.Entity();

            builder.HasIndex([ nameof(UserPermission.UserId), nameof(UserPermission.PermissionId) ])
                .IsUnique();

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
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