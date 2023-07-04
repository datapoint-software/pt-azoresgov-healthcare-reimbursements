using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class UserEntityPermissionEntityTypeConfiguration : IEntityTypeConfiguration<UserEntityPermissionEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntityPermissionEntity> builder)
        {
            builder.Entity();

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(e => e.Entity)
                .WithMany()
                .HasForeignKey(e => e.EntityId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(e => e.Permission)
                .WithMany()
                .HasForeignKey(e => e.PermissionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasIndex(e => e.UserId);

            builder.HasIndex(e => e.EntityId);

            builder.HasIndex(e => e.PermissionId);

            builder.HasAlternateKey(e => new
            {
                e.UserId,
                e.EntityId,
                e.PermissionId
            });

            builder.Property(e => e.Granted)
                .IsRequired();
        }
    }
}
