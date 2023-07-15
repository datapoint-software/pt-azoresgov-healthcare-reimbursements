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

            builder.HasIndex(e => e.UserId);

            builder.HasIndex(e => e.UserAgentId);

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(e => e.UserAgent)
                .WithMany()
                .HasForeignKey(e => e.UserAgentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Property(e => e.NetworkAddress)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(e => e.Start)
                .IsRequired();

            builder.Property(e => e.LastSeen)
                .IsRequired();
        }
    }
}
