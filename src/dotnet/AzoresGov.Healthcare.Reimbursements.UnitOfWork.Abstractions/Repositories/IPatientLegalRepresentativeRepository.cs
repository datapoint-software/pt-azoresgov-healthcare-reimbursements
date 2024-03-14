using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IPatientLegalRepresentativeRepository : IRepository<PatientLegalRepresentative>
    {
        Task<PatientLegalRepresentative?> GetByPatientIdAsync(
            long patientId,
            CancellationToken ct);
    }
}