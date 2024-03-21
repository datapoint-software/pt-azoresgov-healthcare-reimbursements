using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IIasConfigurationRepository : IRepository<IasConfiguration>
    {
        Task<IasConfiguration?> GetByYearAsync(int year, CancellationToken ct);
    }
}
