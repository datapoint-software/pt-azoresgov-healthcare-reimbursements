using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class LegalRepresentativeRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, LegalRepresentative>, ILegalRepresentativeRepository
    {
        public LegalRepresentativeRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<LegalRepresentative?> GetByTaxNumberAsync(string taxNumber, CancellationToken ct)
        {
            return Entities.FirstOrDefaultAsync(e => e.TaxNumber == taxNumber, ct);
        }
    }
}
