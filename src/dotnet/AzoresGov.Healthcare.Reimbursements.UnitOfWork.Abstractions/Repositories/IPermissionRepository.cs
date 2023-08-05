using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IPermissionRepository : IRepository<PermissionEntity>
    {
        Task<IEnumerable<PermissionEntity>> GetAllByIdAsync(IEnumerable<long> id, CancellationToken ct);

        Task<PermissionEntity?> GetByNameAsync(string name, CancellationToken ct);
    }
}
