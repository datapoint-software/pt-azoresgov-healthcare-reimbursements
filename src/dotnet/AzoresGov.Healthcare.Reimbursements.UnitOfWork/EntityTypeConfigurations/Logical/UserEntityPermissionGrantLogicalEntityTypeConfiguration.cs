using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.Logical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations.Logical
{
    public sealed class UserEntityPermissionGrantLogicalEntityTypeConfiguration : IEntityTypeConfiguration<UserEntityPermissionGrantLogicalEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntityPermissionGrantLogicalEntity> builder)
        {
            builder.ToView("UserEntityPermissionGrants");

            builder.HasKey(e => new { e.UserId, e.EntityId, e.PermissionId });

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.EntityId)
                .IsRequired();

            builder.Property(e => e.PermissionId)
                .IsRequired();

            builder.Property(e => e.Granted)
                .IsRequired();
        }
    }
}
