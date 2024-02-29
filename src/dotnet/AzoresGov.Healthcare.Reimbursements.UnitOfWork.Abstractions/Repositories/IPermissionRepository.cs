using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<Permission?> GetByNameAsync(string name, CancellationToken ct);
    }
}