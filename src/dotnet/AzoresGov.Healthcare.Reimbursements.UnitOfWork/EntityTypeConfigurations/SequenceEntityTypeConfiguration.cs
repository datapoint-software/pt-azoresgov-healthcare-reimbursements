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

            builder.HasIndex(e => e.Name)
                .IsUnique();
            
            builder.Property(e => e.Name)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.NextValue)
                .IsRequired();
        }
    }
}