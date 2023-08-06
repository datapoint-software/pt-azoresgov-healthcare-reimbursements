using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.Logical;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations.Logical
{
    public sealed class UserPermissionGrantLogicalEntityTypeConfiguration : IEntityTypeConfiguration<UserPermissionGrantLogicalEntity>
    {
        public void Configure(EntityTypeBuilder<UserPermissionGrantLogicalEntity> builder)
        {
            builder.ToView("UserPermissionGrants");

            builder.HasKey(e => new { e.UserId, e.PermissionId });

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.PermissionId)
                .IsRequired();

            builder.Property(e => e.Granted)
                .IsRequired();
        }
    }
}
