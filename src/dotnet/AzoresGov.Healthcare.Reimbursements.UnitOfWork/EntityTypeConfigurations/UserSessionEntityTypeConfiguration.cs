using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class UserSessionEntityTypeConfiguration : IEntityTypeConfiguration<UserSessionEntity>
    {
        public void Configure(EntityTypeBuilder<UserSessionEntity> builder)
        {
            builder.Entity();

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey("UserId")
                .IsRequired();

            builder.Property(e => e.Agent)
                .HasMaxLength(4096)
                .IsRequired();

            builder.Property(e => e.NetworkAddress)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(e => e.Creation)
                .IsRequired();

            builder.Property(e => e.Expiration);

            builder.Property(e => e.LastSeen)
                .IsRequired();
        }
    }
}
