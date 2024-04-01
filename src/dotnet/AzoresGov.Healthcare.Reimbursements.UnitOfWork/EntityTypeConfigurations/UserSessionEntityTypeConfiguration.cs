using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class UserSessionEntityTypeConfiguration : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder.Entity();

            builder.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<UserSession>(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.UserAgent)
                .HasMaxLength(4092)
                .IsRequired();

            builder.Property(e => e.RemoteAddress)
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
