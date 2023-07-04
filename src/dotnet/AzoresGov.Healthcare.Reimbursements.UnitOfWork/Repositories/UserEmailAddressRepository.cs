using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserEmailAddressRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, UserEmailAddressEntity>, IUserEmailAddressRepository
    {
        public UserEmailAddressRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<UserEmailAddressEntity?> GetWithUserByEmailAddressAsync(string emailAddress, CancellationToken ct) => Entities

            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.EmailAddress == emailAddress, ct);
    }
}
