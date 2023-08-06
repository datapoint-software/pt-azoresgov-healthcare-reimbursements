using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class ParameterRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, ParameterEntity>, IParameterRepository
    {
        public ParameterRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<ParameterEntity>> GetAllByNameAsync(IReadOnlyCollection<string> name, CancellationToken ct) => await Entities

            .Where(e => name.Contains(e.Name))
            .ToListAsync(ct);

        public Task<ParameterEntity?> GetByNameAsync(string name, CancellationToken ct) => Entities

            .FirstOrDefaultAsync(e => e.Name == name, ct);
    }
}
