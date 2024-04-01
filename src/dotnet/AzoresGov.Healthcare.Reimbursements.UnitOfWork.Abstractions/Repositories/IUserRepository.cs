using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAddressAsync(
            string emailAddress, 
            CancellationToken ct);
    }
}
