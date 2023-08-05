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
    public sealed class EntityParentEntityTypeConfiguration : IEntityTypeConfiguration<EntityParentEntity>
    {
        public void Configure(EntityTypeBuilder<EntityParentEntity> builder)
        {
            builder.Entity();

            builder.HasOne(e => e.Entity)
                .WithMany()
                .HasForeignKey(e => e.EntityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.ParentEntity)
                .WithMany()
                .HasForeignKey(e => e.ParentEntityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.Level)
                .IsRequired();
        }
    }
}
