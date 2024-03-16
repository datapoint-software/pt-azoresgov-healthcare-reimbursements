using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IPatientFamilyIncomeStatementRepository : IRepository<PatientFamilyIncomeStatement>
    {
        Task<PatientFamilyIncomeStatement?> GetByPatientIdAsync(
            long patientId,
            CancellationToken ct);
    }
}