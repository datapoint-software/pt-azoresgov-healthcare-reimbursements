using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserPasswordRepository : IRepository<UserPasswordEntity>
    {
        Task<UserPasswordEntity?> GetLastByUserIdAsync(long userId, CancellationToken ct);
    }
}
