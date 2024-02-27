using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsContext, UserEntity>, IUserRepository
    {
        public UserRepository(HealthcareReimbursementsContext context) : base(context)
        {
        }

        public Task<UserEntity?> GetByEmailAddressAsync(string emailAddress, CancellationToken ct) => Entities
            .FirstOrDefaultAsync(e => e.EmailAddress == emailAddress, ct);

        public Task<UserEntity?> GetByUserSessionPublicIdAsync(Guid userSessionPublicId, CancellationToken ct) => Context.UserSessions
            .Where(us => us.PublicId == userSessionPublicId)
            .Select(us => us.User)
            .FirstOrDefaultAsync(ct);
    }
}
