using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IEntityRepository : IRepository<Entity>
    {
        Task<IReadOnlyCollection<Entity>> GetAllByIdAsync(IReadOnlyCollection<long> id, CancellationToken ct);
    }
}