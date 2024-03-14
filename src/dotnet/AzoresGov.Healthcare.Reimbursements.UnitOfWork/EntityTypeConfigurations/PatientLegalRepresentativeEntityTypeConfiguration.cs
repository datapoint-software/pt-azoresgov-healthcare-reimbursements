using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class PatientLegalRepresentativeEntityTypeConfiguration : IEntityTypeConfiguration<PatientLegalRepresentative>
    {
        public void Configure(EntityTypeBuilder<PatientLegalRepresentative> builder)
        {
            builder.Entity();
            
            builder.HasOne(e => e.Patient)
                .WithOne()
                .HasForeignKey<PatientLegalRepresentative>(e => e.PatientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.Property(e => e.Name)
                .HasMaxLength(128)
                .IsRequired();
            
            builder.Property(e => e.TaxNumber)
                .HasMaxLength(16)
                .IsRequired();
            
            builder.Property(e => e.EmailAddress)
                .HasMaxLength(256);

            builder.Property(e => e.FaxNumber)
                .HasMaxLength(16);

            builder.Property(e => e.MobileNumber)
                .HasMaxLength(16);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(16);
        }
    }
}