using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IParameterRepository : IRepository<ParameterEntity>
    {
        Task<ParameterEntity?> GetByNameAsync(string name, CancellationToken ct);
    }
}
