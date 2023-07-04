using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class UserEntityEntityTypeConfiguration : IEntityTypeConfiguration<UserEntityEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntityEntity> builder)
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

            builder.HasIndex(e => e.UserId);

            builder.HasIndex(e => e.EntityId);

            builder.HasAlternateKey(e => new
            {
                e.UserId,
                e.EntityId
            });
        }
    }
}
