using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class UserAgentEntityTypeConfiguration : IEntityTypeConfiguration<UserAgentEntity>
    {
        public void Configure(EntityTypeBuilder<UserAgentEntity> builder)
        {
            builder.Entity();

            builder.HasIndex(e => e.Hash)
                .IsUnique();

            builder.Property(e => e.Hash)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.Signature)
                .HasMaxLength(4096)
                .IsRequired();
        }
    }
}
