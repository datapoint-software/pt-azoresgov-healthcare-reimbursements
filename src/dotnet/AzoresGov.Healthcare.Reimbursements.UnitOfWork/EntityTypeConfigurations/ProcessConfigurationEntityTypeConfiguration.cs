using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class ProcessConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<ProcessConfiguration>
    {
        public void Configure(EntityTypeBuilder<ProcessConfiguration> builder)
        {
            builder.Entity();

            builder.HasOne(e => e.Process)
                .WithOne()
                .HasForeignKey<ProcessConfiguration>(e => e.ProcessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.MachadoJosephEnabled)
                .IsRequired();

            builder.Property(e => e.ReimbursementLimitBypassEnabled)
                .IsRequired();

            builder.Property(e => e.DocumentIssueDateBypassEnabled)
                .IsRequired();
        }
    }
}