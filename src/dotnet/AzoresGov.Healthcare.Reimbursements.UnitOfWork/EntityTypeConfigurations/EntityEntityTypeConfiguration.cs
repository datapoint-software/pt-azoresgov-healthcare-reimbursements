using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class EntityEntityTypeConfiguration : IEntityTypeConfiguration<EntityEntity>
    {
        public void Configure(EntityTypeBuilder<EntityEntity> builder)
        {
            builder.Entity();

            builder.HasIndex(e => e.Code)
                .IsUnique();

            builder.Property(e => e.Code)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(e => e.Kind)
                .IsRequired();
        }
    }
}
