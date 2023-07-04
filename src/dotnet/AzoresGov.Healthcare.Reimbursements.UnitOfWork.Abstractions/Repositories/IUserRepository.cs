using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity?> GetByUserEmailAddressIdAsync(long userId, CancellationToken ct);
    }
}
