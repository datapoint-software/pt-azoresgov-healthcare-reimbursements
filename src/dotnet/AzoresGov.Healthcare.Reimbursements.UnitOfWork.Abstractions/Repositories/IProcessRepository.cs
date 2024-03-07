using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IProcessRepository : IRepository<Process>
    {
        Task<int> CountBySearchCriteriaAsync(
            string? filter,
            IReadOnlyCollection<long>? entityId,
            IReadOnlyCollection<ProcessStatus>? status,
            CancellationToken ct);
        
        Task<IReadOnlyCollection<Process>> GetAllBySearchCriteriaAsync(
            string? filter,
            IReadOnlyCollection<long>? entityId,
            IReadOnlyCollection<ProcessStatus>? status,
            int skip,
            int take,
            CancellationToken ct);
    }
}