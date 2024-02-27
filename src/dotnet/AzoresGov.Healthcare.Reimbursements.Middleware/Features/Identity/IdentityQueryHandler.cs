using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityQueryHandler : IQueryHandler<IdentityQuery, IdentityResult>
    {
        private readonly IUserRepository _users;

        public IdentityQueryHandler(IUserRepository users)
        {
            _users = users;
        }

        public async Task<IdentityResult> HandleQueryAsync(IdentityQuery query, CancellationToken ct)
        {
            var user = await _users.GetByUserSessionPublicIdOrThrowBusinessExceptionAsync(
                query.UserSessionId,
                ct);

            return new IdentityResult(
                new IdentityUserResult(
                    user.PublicId,
                    user.Name,
                    user.EmailAddress));
        }
    }
}
