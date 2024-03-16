using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class ProcessPaymentConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<ProcessPaymentConfiguration>
    {
        public void Configure(EntityTypeBuilder<ProcessPaymentConfiguration> builder)
        {
            builder.Entity();

            builder.HasOne(e => e.Process)
                .WithOne()
                .HasForeignKey<ProcessPaymentConfiguration>(e => e.ProcessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.Method)
                .IsRequired();

            builder.Property(e => e.Receiver)
                .IsRequired();
        }
    }
}