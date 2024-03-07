using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<int> CountByEntitySearchCriteriaAsync(
            long entityId,
            string? filter,
            CancellationToken ct);

        Task<IReadOnlyCollection<Patient>> GetAllByEntitySearchCriteriaAsync(
            long entityId,
            string? filter,
            int skip,
            int take,
            CancellationToken ct);

        Task<IReadOnlyCollection<Patient>> GetAllByIdAsync(
            IReadOnlyCollection<long> id,
            CancellationToken ct);
    }
}