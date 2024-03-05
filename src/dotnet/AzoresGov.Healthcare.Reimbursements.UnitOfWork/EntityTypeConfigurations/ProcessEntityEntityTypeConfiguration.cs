using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class ProcessEntityEntityTypeConfiguration : IEntityTypeConfiguration<ProcessEntity>
    {
        public void Configure(EntityTypeBuilder<ProcessEntity> builder)
        {
            builder.Entity();

            builder.HasIndex([ nameof(ProcessEntity.ProcessId), nameof(ProcessEntity.EntityId) ])
                .IsUnique();

            builder.HasOne(e => e.Process)
                .WithMany()
                .HasForeignKey(e => e.ProcessId)
                .IsRequired();

            builder.HasOne(e => e.Entity)
                .WithMany()
                .HasForeignKey(e => e.EntityId)
                .IsRequired();
        }
    }
}