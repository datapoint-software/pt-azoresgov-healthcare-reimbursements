using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System;
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
            var user = await _users.GetByPublicIdOrThrowExceptionAsync(
                query.UserId,
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
