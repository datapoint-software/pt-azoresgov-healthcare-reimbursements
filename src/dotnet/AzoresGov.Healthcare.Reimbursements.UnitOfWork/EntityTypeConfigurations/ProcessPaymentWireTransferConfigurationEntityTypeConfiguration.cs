using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class ProcessPaymentWireTransferConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<ProcessPaymentWireTransferConfiguration>
    {
        public void Configure(EntityTypeBuilder<ProcessPaymentWireTransferConfiguration> builder)
        {
            builder.Entity();

            builder.HasOne(e => e.Process)
                .WithOne()
                .HasForeignKey<ProcessPaymentWireTransferConfiguration>(e => e.ProcessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.Iban)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(e => e.Swift)
                .HasMaxLength(16)
                .IsRequired();
        }
    }
}