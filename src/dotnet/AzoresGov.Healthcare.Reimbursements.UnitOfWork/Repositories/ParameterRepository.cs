using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class ParameterRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, Parameter>, IParameterRepository
    {
        public ParameterRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<Parameter?> GetByNameAsync(string name, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.Name == name, ct);
    }
}
