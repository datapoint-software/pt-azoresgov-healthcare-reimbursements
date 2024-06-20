using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IProcessRepository : IRepository<Process>
    {
        Task<IReadOnlyCollection<Process>> GetAllByBasicSearchCriteriaAsync(
            IReadOnlyCollection<long> entityIds,
            string filter,
            int skip,
            int take,
            CancellationToken ct);

        Task<IReadOnlyCollection<Process>> GetAllByEmptySearchCriteriaAsync(
            IReadOnlyCollection<long> entityIds,
            int skip,
            int take,
            CancellationToken ct);

        Task<IReadOnlyCollection<Process>> GetAllByFullSearchCriteriaAsync(
            IReadOnlyCollection<long> entityIds,
            string filter,
            int skip,
            int take,
            CancellationToken ct);

        Task<int> GetCountByBasicSearchCriteriaAsync(
            IReadOnlyCollection<long> entityIds,
            string filter,
            CancellationToken ct);

        Task<int> GetCountByEmptySearchCriteriaAsync(
            IReadOnlyCollection<long> entityIds,
            CancellationToken ct);

        Task<int> GetCountByFullSearchCriteriaAsync(
            IReadOnlyCollection<long> entityIds,
            string filter,
            CancellationToken ct);
    }
}
