using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IProcessConfigurationRepository : IRepository<ProcessConfiguration>
    {
        Task<ProcessConfiguration?> GetByProcessIdAsync(long processId, CancellationToken ct);

        Task<IReadOnlyCollection<ProcessConfiguration>> GetAllByProcessIdAsync(
            IReadOnlyCollection<long> processId, 
            CancellationToken ct);
    }
}