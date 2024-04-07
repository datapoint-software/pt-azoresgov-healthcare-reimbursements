using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class EntityParentEntityEntityTypeConfiguration : IEntityTypeConfiguration<EntityParentEntity>
    {
        public void Configure(EntityTypeBuilder<EntityParentEntity> builder)
        {
            builder.Entity();

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

            builder.HasIndex(e => new { e.EntityId, e.ParentEntityId })
                .IsUnique();

            builder.HasIndex(e => new { e.EntityId, e.Level })
                .IsUnique();

            builder.Property(e => e.Level)
                .IsRequired();
        }
    }
}
