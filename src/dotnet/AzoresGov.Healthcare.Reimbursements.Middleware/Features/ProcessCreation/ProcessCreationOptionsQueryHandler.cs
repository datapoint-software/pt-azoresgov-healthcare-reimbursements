using AzoresGov.Healthcare.Reimbursements.Middleware.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Managers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationOptionsQueryHandler : IQueryHandler<ProcessCreationOptionsQuery, ProcessCreationOptionsResult>
    {
        private readonly AuthorizationManager _authorization;

        private readonly IUserRepository _users;

        public ProcessCreationOptionsQueryHandler(AuthorizationManager authorization, IUserRepository users)
        {
            _authorization = authorization;
            _users = users;
        }

        public async Task<ProcessCreationOptionsResult> HandleQueryAsync(ProcessCreationOptionsQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowBusinessExceptionAsync(
                query.UserId,
                ct);

            await _authorization.AuthorizeOrThrowBusinessExceptionAsync(
                PermissionName.ProcessCreation,
                user,
                ct);

            var entitySelectionEnabled = await HasZeroOrMultiplePermissionGrantsAsync(
                user,
                ct);

            return new ProcessCreationOptionsResult(
                new ProcessCreationEntitySelectionOptionsResult(
                    entitySelectionEnabled));
        }

        private async Task<bool> HasZeroOrMultiplePermissionGrantsAsync(UserEntity user, CancellationToken ct)
        {
            var grantCount = await _authorization.CountUserEntityPermissionGrantsAsync(
                PermissionName.ProcessCreation,
                user,
                ct);

            return grantCount is 0 or > 1;
        }
    }
}
