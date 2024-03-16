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
            var user = await _users.GetByPublicIdOrThrowBusinessExceptionAsync(
                command.UserId,
                ct);

            var process = await _processes.GetByPublicIdOrThrowBusinessExceptionAsync(
                command.ProcessId,
                ct);

            Assert.RowVersion(
                process.RowVersionId,
                command.ProcessRowVersionId);

            Assert.ProcessStatus(
                ProcessStatus.Capture, 
                process.Status);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user.Id,
                process.EntityId,
                ct);

            var processConfiguration = await GetOrCreateProcessConfigurationAsync(
                process,
                command.ProcessConfigurationRowVersionId,
                ct);

            processConfiguration.MachadoJosephEnabled = command.MachadoJosephEnabled;
            processConfiguration.DocumentIssueDateBypassEnabled = command.DocumentIssueDateBypassEnabled;
            processConfiguration.ReimbursementLimitBypassEnabled = command.ReimbursementLimitBypassEnabled;
            processConfiguration.UnemploymentEnabled = command.UnemploymentEnabled;
            
            process.RowVersionId = Guid.NewGuid();
            processConfiguration.RowVersionId = Guid.NewGuid();

            return new ProcessCaptureConfigurationResult(
                process.RowVersionId,
                processConfiguration.RowVersionId);
        }

        private async Task<ProcessConfiguration> GetOrCreateProcessConfigurationAsync(
            Process process,
            Guid? processConfigurationRowVersionId,
            CancellationToken ct)
        {
            var processConfiguration = await _processSettings.GetByProcessIdAsync(
                process.Id,
                ct);

            if (processConfiguration is null)
            {
                return _processSettings.Add(new ProcessConfiguration()
                {
                    Process = process
                });
            }
            
            Assert.RowVersion(
                processConfiguration.RowVersionId,
                processConfigurationRowVersionId);

            return processConfiguration;
        }
    }
}