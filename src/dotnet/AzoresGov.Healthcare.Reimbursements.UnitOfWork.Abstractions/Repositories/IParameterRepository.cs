using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IParameterRepository : IRepository<ParameterEntity>
    {
        Task<IReadOnlyCollection<ParameterEntity>> GetAllByNameAsync(IReadOnlyCollection<string> name, CancellationToken ct);

        Task<ParameterEntity?> GetByNameAsync(string name, CancellationToken ct);
    }
}
