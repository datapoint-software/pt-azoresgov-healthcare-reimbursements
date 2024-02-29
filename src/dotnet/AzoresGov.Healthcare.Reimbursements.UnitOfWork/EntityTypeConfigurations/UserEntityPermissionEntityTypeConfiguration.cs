using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class UserEntityPermissionEntityTypeConfiguration : IEntityTypeConfiguration<UserEntityPermission>
    {
        public void Configure(EntityTypeBuilder<UserEntityPermission> builder)
        {
            builder.Entity();

            builder.HasIndex([ nameof(UserEntityPermission.UserId), nameof(UserEntityPermission.EntityId), nameof(UserEntityPermission.PermissionId) ])
                .IsUnique();

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            builder.HasOne(e => e.Entity)
                .WithMany()
                .HasForeignKey(e => e.EntityId)
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