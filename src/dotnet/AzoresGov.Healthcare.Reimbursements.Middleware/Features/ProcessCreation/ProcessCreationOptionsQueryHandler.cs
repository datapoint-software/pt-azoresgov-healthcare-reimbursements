using AzoresGov.Healthcare.Reimbursements.Middleware.Constants;
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

        public ProcessCreationOptionsQueryHandler(AuthorizationManager authorization, IPermissionRepository permissions, IUserRepository users)
        {
            _authorization = authorization;
            _users = users;
        }

        public async Task<ProcessCreationOptionsResult> HandleQueryAsync(ProcessCreationOptionsQuery query, CancellationToken ct)
        {
            var user = await GetUserAsync(
                query,
                ct);

            await AuthorizeAsync(
                user, 
                ct);

            var entitySelectionEnabled = await HasZeroOrMultiplePermissionGrantsAsync(
                user,
                ct);

            return new ProcessCreationOptionsResult(
                new ProcessCreationEntitySelectionOptionsResult(
                    entitySelectionEnabled));
        }

        private async Task AuthorizeAsync(UserEntity user, CancellationToken ct)
        {
            var result = await _authorization.AuthorizeAsync(
                PermissionName.ProcessCreation,
                user,
                ct);

            if (result == false)
            {
                throw new AuthorizationException("User has insufficient permissions.")
                    .WithUserMessage("O seu perfil não tem permissões suficientes para registar um processo de reembolso.")
                    .WithCode("TEHBXC");
            }
        }

        private async Task<bool> HasZeroOrMultiplePermissionGrantsAsync(UserEntity user, CancellationToken ct)
        {
            var grantCount = await _authorization.CountUserEntityPermissionGrantsAsync(
                PermissionName.ProcessCreation,
                user,
                ct);

            return grantCount == 0 || grantCount > 1;
        }

        private async Task<UserEntity> GetUserAsync(ProcessCreationOptionsQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                query.UserId,
                ct);

            if (user == null)
            {
                throw new BusinessException("A user was not found matching the given identifier.")
                    .WithUserMessage("O perfil do utilizador não foi encontrado.")
                    .WithCode("A09NHF");
            }

            return user;
        }
    }
}
