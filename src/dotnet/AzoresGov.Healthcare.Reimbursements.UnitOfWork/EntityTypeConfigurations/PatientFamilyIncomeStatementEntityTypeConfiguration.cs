using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.EntityTypeConfigurations
{
    public sealed class PatientFamilyIncomeStatementEntityTypeConfiguration : IEntityTypeConfiguration<PatientFamilyIncomeStatement>
    {
        public void Configure(EntityTypeBuilder<PatientFamilyIncomeStatement> builder)
        {
            builder.Entity();

            builder.HasOne(e => e.Patient)
                .WithOne()
                .HasForeignKey<PatientFamilyIncomeStatement>(e => e.PatientId)
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