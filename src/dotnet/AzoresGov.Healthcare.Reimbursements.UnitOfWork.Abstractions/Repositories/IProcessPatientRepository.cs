using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IProcessPatientRepository : IRepository<ProcessPatient>
    {
        Task<IReadOnlyCollection<ProcessPatient>> GetAllByProcessIdAsync(
            IReadOnlyCollection<long> processId,
            CancellationToken ct);
        
        Task<ProcessPatient?> GetByProcessIdAsync(
            long processId,
            CancellationToken ct);
    }
}