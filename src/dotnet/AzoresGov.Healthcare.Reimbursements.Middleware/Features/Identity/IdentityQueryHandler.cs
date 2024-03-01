using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityQueryHandler : IQueryHandler<IdentityQuery, IdentityResult>
    {
        private readonly IUserRepository _users;

        private readonly IRoleRepository _roles;

        public IdentityQueryHandler(IUserRepository users, IRoleRepository roles)
        {
            _users = users;
            _roles = roles;
        }

        public async Task<IdentityResult> HandleQueryAsync(IdentityQuery query, CancellationToken ct)
        {
            var user = await _users.GetByUserSessionPublicIdOrThrowBusinessExceptionAsync(
                query.UserSessionId,
                ct);

            var roles = await _roles.GetAllByUserIdAsync(
                user.Id,
                ct);

            return new IdentityResult(
                roles
                    .Select(r => new IdentityRoleResult(
                        r.PublicId,
                        r.RowVersionId,
                        r.Name))
                    .ToArray(),
                new IdentityUserResult(
                    user.PublicId,
                    user.Name,
                    user.EmailAddress));
        }
    }
}
