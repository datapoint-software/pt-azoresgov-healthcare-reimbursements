using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class IasConfigurationEntityTypeConfiguration : IEntityTypeConfiguration<IasConfiguration>
    {
        public void Configure(EntityTypeBuilder<IasConfiguration> builder)
        {
            builder.Entity();

            builder.HasIndex(e => e.Year)
                .IsUnique();

            builder.Property(e => e.Year)
                .IsRequired();

            builder.Property(e => e.Amount)
                .HasPrecision(8, 2)
                .IsRequired();
        }
    }
}
