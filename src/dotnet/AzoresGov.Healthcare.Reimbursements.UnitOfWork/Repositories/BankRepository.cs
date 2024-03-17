using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class BankRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, Bank>, IBankRepository
    {
        public BankRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<Bank?> GetBySwiftCodeAsync(string swiftCode, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.SwiftCode == swiftCode, ct);

        public Task<Bank?> GetBySwiftLookupCodeAsync(string swiftLookupCode, CancellationToken ct) => 

            Entities.FirstOrDefaultAsync(e => e.SwiftLookupCode == swiftLookupCode, ct);
    }
}