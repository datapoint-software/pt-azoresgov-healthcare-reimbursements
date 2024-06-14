using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class SequenceEntityTypeConfiguration : IEntityTypeConfiguration<Sequence>
    {
        public void Configure(EntityTypeBuilder<Sequence> builder)
        {
            builder.Entity();

            builder.HasAlternateKey(e => e.Name);

            builder.Property(e => e.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(e => e.NextValue)
                .IsRequired();
        }
    }
}
