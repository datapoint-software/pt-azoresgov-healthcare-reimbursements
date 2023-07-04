using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserEmailAddressRepository : IRepository<UserEmailAddressEntity>
    {
        Task<UserEmailAddressEntity?> GetWithUserByEmailAddressAsync(string emailAddress, CancellationToken ct);
    }
}
