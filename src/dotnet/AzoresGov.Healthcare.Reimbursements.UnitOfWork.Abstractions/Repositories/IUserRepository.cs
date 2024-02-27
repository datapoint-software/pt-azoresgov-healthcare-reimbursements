using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity?> GetByEmailAddressAsync(string emailAddress, CancellationToken ct);

        Task<UserEntity?> GetByUserSessionPublicIdAsync(Guid userSessionPublicId, CancellationToken ct);
    }
}
