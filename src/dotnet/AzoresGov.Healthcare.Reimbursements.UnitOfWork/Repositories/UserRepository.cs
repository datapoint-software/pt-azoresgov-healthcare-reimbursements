using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, UserEntity>, IUserRepository
    {
        public UserRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<UserEntity?> GetByUserEmailAddressIdAsync(long userEmailAddressId, CancellationToken ct) => Context.UserEmailAddresses

            .Where(e => e.Id == userEmailAddressId)
            .Select(e => e.User)
            .FirstOrDefaultAsync(ct);
    }
}
