using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class EntityParentEntityEntityTypeConfiguration : IEntityTypeConfiguration<EntityParentEntity>
    {
        public void Configure(EntityTypeBuilder<EntityParentEntity> builder)
        {
            builder.Entity();

            builder.HasIndex([ nameof(EntityParentEntity.EntityId), nameof(EntityParentEntity.ParentEntityId) ])
                .IsUnique();

            builder.HasIndex([ nameof(EntityParentEntity.EntityId), nameof(EntityParentEntity.Level) ])
                .IsUnique();

            builder.HasOne(e => e.Entity)
                .WithMany()
                .HasForeignKey(e => e.EntityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ParentEntity)
                .WithMany()
                .HasForeignKey(e => e.ParentEntityId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.Property(e => e.Level)
                .IsRequired();
        }
    }
}