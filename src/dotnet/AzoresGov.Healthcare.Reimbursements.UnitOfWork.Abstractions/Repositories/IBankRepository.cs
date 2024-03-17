using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IBankRepository : IRepository<Bank>
    {
        Task<Bank?> GetBySwiftCodeAsync(string swiftCode, CancellationToken ct);
        
        Task<Bank?> GetBySwiftLookupCodeAsync(string swiftLookupCode, CancellationToken ct);
    }
}