using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserAgentRepository : IRepository<UserAgentEntity>
    {
        Task<UserAgentEntity?> GetByHashAsync(string hash, CancellationToken ct);
    }
}
