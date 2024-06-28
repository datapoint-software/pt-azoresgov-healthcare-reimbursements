using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class ProcessEntityTypeConfiguration : IEntityTypeConfiguration<Process>
    {
        public void Configure(EntityTypeBuilder<Process> builder)
        {
            builder.Entity();

            builder.HasAlternateKey(e => e.Number);

            builder.HasOne(e => e.Entity)
                .WithMany()
                .HasForeignKey(e => e.EntityId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne(e => e.Patient)
                .WithMany()
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.LegalRepresentative)
                .WithMany()
                .HasForeignKey(e => e.LegalRepresentativeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Number)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(e => e.Status)
                .IsRequired();

            builder.Property(e => e.Creation)
                .IsRequired();
        }
    }
}
