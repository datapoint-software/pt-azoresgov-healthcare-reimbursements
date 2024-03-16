using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class ProcessPatientFamilyIncomeStatementEntityTypeConfiguration : IEntityTypeConfiguration<ProcessPatientFamilyIncomeStatement>
    {
        public void Configure(EntityTypeBuilder<ProcessPatientFamilyIncomeStatement> builder)
        {
            builder.Entity();

            builder.HasOne(e => e.Process)
                .WithOne()
                .HasForeignKey<ProcessPatientFamilyIncomeStatement>(e => e.ProcessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.Year)
                .IsRequired();

            builder.Property(e => e.AccessCode)
                .HasMaxLength(16);

            builder.Property(e => e.FamilyMemberCount)
                .IsRequired();

            builder.Property(e => e.FamilyIncome)
                .HasPrecision(14, 2)
                .IsRequired();
        }
    }
}