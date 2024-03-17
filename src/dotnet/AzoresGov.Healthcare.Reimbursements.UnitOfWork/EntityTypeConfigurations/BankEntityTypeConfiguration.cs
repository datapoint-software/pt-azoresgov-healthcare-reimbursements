using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class BankEntityTypeConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.Entity();

            builder.HasIndex(e => e.SwiftCode)
                .IsUnique();

            builder.HasIndex(e => e.SwiftLookupCode)
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.SwiftCode)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.SwiftLookupCode)
                .HasMaxLength(16);
        }
    }
}