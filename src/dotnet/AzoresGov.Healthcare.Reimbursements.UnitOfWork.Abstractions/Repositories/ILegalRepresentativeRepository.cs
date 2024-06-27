using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface ILegalRepresentativeRepository : IRepository<LegalRepresentative>
    {
        Task<LegalRepresentative?> GetByTaxNumberAsync(
            string taxNumber, 
            CancellationToken ct);
    }
}
