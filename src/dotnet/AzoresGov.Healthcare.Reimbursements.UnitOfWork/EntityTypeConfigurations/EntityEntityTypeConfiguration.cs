using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class EntityEntityTypeConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.Entity();

            builder.HasIndex(e => e.Code);

            builder.HasIndex(e => e.Name);

            builder.Property(e => e.Code)
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.Nature)
                .IsRequired();
        }
    }
}