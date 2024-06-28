using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Entity();

            builder.HasIndex(e => e.Number)
                .IsUnique();

            builder.HasIndex(e => e.TaxNumber);

            builder.HasIndex(e => e.Name);

            builder.HasIndex(e => e.EmailAddress);

            builder.HasIndex(e => e.FaxNumber);

            builder.HasIndex(e => e.MobileNumber);

            builder.HasIndex(e => e.PhoneNumber);

            builder.HasOne(e => e.Entity)
                .WithMany()
                .HasForeignKey(e => e.EntityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.LegalRepresentative)
                .WithMany()
                .HasForeignKey(e => e.LegalRepresentativeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Number)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.TaxNumber)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(e => e.Birth)
                .IsRequired();

            builder.Property(e => e.Death);

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
