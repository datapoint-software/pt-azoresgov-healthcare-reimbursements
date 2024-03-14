using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IProcessPatientLegalRepresentativeRepository : IRepository<ProcessPatientLegalRepresentative>
    {
        Task<ProcessPatientLegalRepresentative?> GetByProcessIdAsync(
            long processId,
            CancellationToken ct);
    }
}