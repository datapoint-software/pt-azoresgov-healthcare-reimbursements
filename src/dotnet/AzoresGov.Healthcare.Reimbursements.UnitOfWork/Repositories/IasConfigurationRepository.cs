using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class IasConfigurationRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, IasConfiguration>, IIasConfigurationRepository
    {
        public IasConfigurationRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<IasConfiguration?> GetByYearAsync(int year, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.Year == year, ct);
    }
}
