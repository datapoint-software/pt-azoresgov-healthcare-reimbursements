using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class PatientFamilyIncomeStatementRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, PatientFamilyIncomeStatement>, IPatientFamilyIncomeStatementRepository
    {
        public PatientFamilyIncomeStatementRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<PatientFamilyIncomeStatement?> GetByPatientIdAsync(
            long patientId,
            CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.PatientId == patientId, ct);
    }
}