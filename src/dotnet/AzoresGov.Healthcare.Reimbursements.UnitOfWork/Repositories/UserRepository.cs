using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, User>, IUserRepository
    {
        public UserRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<User?> GetByEmailAddressAsync(string emailAddress, CancellationToken ct) => 
            
            Entities.FirstOrDefaultAsync(e => e.EmailAddress == emailAddress, ct);

        public Task<User?> GetByUserSessionPublicIdAsync(Guid userSessionPublicId, CancellationToken ct) => 
            
            UnitOfWork.UserSessions
                .Where(us => us.PublicId == userSessionPublicId)
                .Select(us => us.User)
                .FirstOrDefaultAsync(ct);
    }
}
