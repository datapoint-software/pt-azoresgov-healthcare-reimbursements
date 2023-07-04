using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserPasswordRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, UserPasswordEntity>, IUserPasswordRepository
    {
        public UserPasswordRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<UserPasswordEntity?> GetLastByUserIdAsync(long userId, CancellationToken ct) => Entities

            .OrderByDescending(e => e.Id)
            .FirstOrDefaultAsync(e => e.User.Id == userId, ct);
    }
}
