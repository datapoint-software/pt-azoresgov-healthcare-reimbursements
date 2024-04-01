using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, User>, IUserRepository
    {
        public UserRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<User?> GetByEmailAddressAsync(string emailAddress, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.EmailAddress == emailAddress, ct);
    }
}
