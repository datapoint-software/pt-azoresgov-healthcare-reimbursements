using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class UserEmailAddressEntityTypeConfiguration : IEntityTypeConfiguration<UserEmailAddressEntity>
    {
        public void Configure(EntityTypeBuilder<UserEmailAddressEntity> builder)
        {
            builder.Entity();

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasIndex(e => e.UserId);

            builder.HasIndex(e => e.EmailAddress)
                .IsUnique();

            builder.Property(e => e.EmailAddress)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
