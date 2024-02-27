using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class ParameterRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsContext, ParameterEntity>, IParameterRepository
    {
        public ParameterRepository(HealthcareReimbursementsContext context) : base(context)
        {
        }

        public Task<ParameterEntity?> GetByNameAsync(string name, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.Name == name, ct);
    }
}
