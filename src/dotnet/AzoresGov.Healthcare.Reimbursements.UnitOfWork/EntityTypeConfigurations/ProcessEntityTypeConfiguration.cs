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

            builder.HasIndex(e => e.Number)
                .IsUnique();

            builder.HasIndex(e => e.Creation);

            builder.HasIndex(e => e.Expiration);

            builder.HasIndex(e => e.Touch);

            builder.HasOne(e => e.Entity)
                .WithMany()
                .HasForeignKey(e => e.EntityId)
                .IsRequired();

            builder.HasOne(e => e.Patient)
                .WithMany()
                .HasForeignKey(e => e.PatientId)
                .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.Number)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(e => e.Creation)
                .IsRequired();

            builder.Property(e => e.Expiration)
                .IsRequired();

            builder.Property(e => e.Touch)
                .IsRequired();
        }
    }
}