using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IEntityRepository : IRepository<EntityEntity>
    {
        Task<IEnumerable<EntityEntity>> GetAllByIdAsync(IReadOnlyCollection<long> id, CancellationToken ct);

        Task<EntityEntity?> GetByCodeAsync(string code, CancellationToken ct);
    }
}
