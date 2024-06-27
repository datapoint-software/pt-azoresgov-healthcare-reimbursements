using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class LegalRepresentativeEntityTypeConfiguration : IEntityTypeConfiguration<LegalRepresentative>
    {
        public void Configure(EntityTypeBuilder<LegalRepresentative> builder)
        {
            builder.Entity();

            builder.HasIndex(e => e.TaxNumber);

            builder.HasIndex(e => e.Name);

            builder.HasIndex(e => e.EmailAddress);

            builder.HasIndex(e => e.FaxNumber);

            builder.HasIndex(e => e.MobileNumber);

            builder.HasIndex(e => e.PhoneNumber);

            builder.Property(e => e.TaxNumber)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(e => e.FaxNumber)
                .HasMaxLength(16);

            builder.Property(e => e.MobileNumber)
                .HasMaxLength(16);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(16);

            builder.Property(e => e.EmailAddress)
                .HasMaxLength(256);

            builder.Property(e => e.PostalAddressArea)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.PostalAddressAreaCode)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(e => e.PostalAddressLine1)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(e => e.PostalAddressLine2)
                .HasMaxLength(256);

            builder.Property(e => e.PostalAddressLine3)
                .HasMaxLength(256);
        }
    }
}
