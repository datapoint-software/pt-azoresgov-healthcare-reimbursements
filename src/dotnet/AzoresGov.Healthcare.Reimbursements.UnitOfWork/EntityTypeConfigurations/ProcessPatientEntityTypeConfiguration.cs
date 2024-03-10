using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class ProcessPatientEntityTypeConfiguration : IEntityTypeConfiguration<ProcessPatient>
    {
        public void Configure(EntityTypeBuilder<ProcessPatient> builder) 
        {
            builder.Entity();

            builder.HasOne(e => e.Process)
                .WithOne()
                .HasForeignKey<ProcessPatient>(e => e.ProcessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(e => e.Name);

            builder.HasIndex(e => e.HealthNumber);
            
            builder.HasIndex(e => e.TaxNumber);

            builder.HasIndex(e => e.EmailAddress);

            builder.HasIndex(e => e.MobileNumber);
            
            builder.HasIndex(e => e.PhoneNumber);

            builder.Property(e => e.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.Birth);

            builder.Property(e => e.Gender);

            builder.Property(e => e.HealthNumber)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.TaxNumber)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.AddressLine1)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.AddressLine2)
                .HasMaxLength(128);

            builder.Property(e => e.AddressLine3)
                .HasMaxLength(128);

            builder.Property(e => e.PostalCode)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.PostalCodeArea)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.EmailAddress)
                .HasMaxLength(256);

            builder.Property(e => e.FaxNumber)
                .HasMaxLength(16);

            builder.Property(e => e.MobileNumber)
                .HasMaxLength(16);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(16);

            builder.Property(e => e.Death);
        }
    }
}