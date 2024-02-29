using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class UserEntityRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserEntityRole>
    {
        public void Configure(EntityTypeBuilder<UserEntityRole> builder)
        {
            builder.Entity();

            builder.HasIndex([ nameof(UserEntityRole.UserId), nameof(UserEntityRole.EntityId), nameof(UserEntityRole.RoleId) ])
                .IsUnique();

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            builder.HasOne(e => e.Entity)
                .WithMany()
                .HasForeignKey(e => e.EntityId)
                .IsRequired();

            builder.HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleId)
                .IsRequired();
        }
    }
}