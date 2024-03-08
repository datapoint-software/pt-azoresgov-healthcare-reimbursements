using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureConfigurationCommandHandler : ICommandHandler<ProcessCaptureConfigurationCommand, ProcessCaptureConfigurationResult>
    {
        private readonly IProcessRepository _processes;

        private readonly IProcessConfigurationRepository _processSettings;
        
        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCaptureConfigurationCommandHandler(IProcessRepository processes, IProcessConfigurationRepository processSettings, IUserEntityRepository userEntities, IUserRepository users)
        {
            _processes = processes;
            _processSettings = processSettings;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCaptureConfigurationResult> HandleCommandAsync(ProcessCaptureConfigurationCommand command, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowExceptionAsync(
                command.UserId,
                ct);

            var process = await _processes.GetByPublicIdOrThrowExceptionAsync(
                command.ProcessId,
                command.ProcessRowVersionId,
                ct);

            Assert.ProcessStatus(
                ProcessStatus.Capture, 
                process.Status);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user.Id,
                process.EntityId,
                ct);

            var processConfiguration = await _processSettings.GetByProcessIdAsync(
                process.Id,
                ct);

            processConfiguration ??= _processSettings.Add(
                new ProcessConfiguration());

            processConfiguration.MachadoJosephEnabled = command.MachadoJosephEnabled;
            processConfiguration.DocumentIssueDateBypassEnabled = command.DocumentIssueDateBypassEnabled;
            processConfiguration.ReimbursementLimitBypassEnabled = command.ReimbursementLimitBypassEnabled;
            
            process.RowVersionId = Guid.NewGuid();

            return new ProcessCaptureConfigurationResult(
                process.RowVersionId);
        }
    }
}