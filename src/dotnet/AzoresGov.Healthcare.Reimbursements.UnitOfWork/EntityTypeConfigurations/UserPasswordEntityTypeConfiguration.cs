using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class UserPasswordEntityTypeConfiguration : IEntityTypeConfiguration<UserPasswordEntity>
    {
        public void Configure(EntityTypeBuilder<UserPasswordEntity> builder)
        {
            builder.Entity();

            builder.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<UserPasswordEntity>("UserId")
                .IsRequired();

            builder.Property(e => e.Hash)
                .HasMaxLength(64)
                .IsRequired();
        }
    }
}
