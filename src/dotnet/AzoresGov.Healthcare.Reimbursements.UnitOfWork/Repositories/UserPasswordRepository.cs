using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserPasswordRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsContext, UserPasswordEntity>, IUserPasswordRepository
    {
        public UserPasswordRepository(HealthcareReimbursementsContext context) : base(context)
        {
        }

        public Task<UserPasswordEntity?> GetByUserIdAsync(long userId, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.User.Id == userId, ct);
    }
}
