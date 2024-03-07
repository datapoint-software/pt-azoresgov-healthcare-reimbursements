using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessSearch
{
    public sealed class ProcessSearchOptionsQueryHandler : IQueryHandler<ProcessSearchOptionsQuery, ProcessSearchOptionsResult>
    {
        private readonly IEntityRepository _entities;

        private readonly IUserRepository _users;

        public ProcessSearchOptionsQueryHandler(IEntityRepository entities, IUserRepository users)
        {
            _entities = entities;
            _users = users;
        }

        public async Task<ProcessSearchOptionsResult> HandleQueryAsync(ProcessSearchOptionsQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowExceptionAsync(
                query.UserId,
                ct);

            var entities = await _entities.GetAllByUserIdAndNatureAsync(
                user.Id,
                [ EntityNature.HealthCenter, EntityNature.Office ],
                ct);

            return new ProcessSearchOptionsResult(
                entities
                    .Select(e => new ProcessSearchOptionsEntityResult(
                        e.PublicId,
                        e.Name))
                    .ToArray());
        }
    }
}