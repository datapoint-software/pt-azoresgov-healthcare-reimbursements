using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureCompleteCommandHandler : ICommandHandler<ProcessCaptureCompleteCommand, ProcessCaptureCompleteResult>
    {
        private readonly IProcessRepository _processes;

        private readonly IProcessPaymentConfigurationRepository _processPaymentConfiguration;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public ProcessCaptureCompleteCommandHandler(IProcessRepository processes, IProcessPaymentConfigurationRepository processPaymentConfiguration, IUserEntityRepository userEntities, IUserRepository users)
        {
            _processes = processes;
            _processPaymentConfiguration = processPaymentConfiguration;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCaptureCompleteResult> HandleCommandAsync(ProcessCaptureCompleteCommand command, CancellationToken ct)
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

            var processPaymentConfiguration = await _processPaymentConfiguration.GetByProcessIdAsync(
                process.Id,
                ct);

            Assert.Found(processPaymentConfiguration);

            process.Status = ProcessStatus.DocumentUpload;

            process.RowVersionId = Guid.NewGuid();

            return new ProcessCaptureCompleteResult(
                process.RowVersionId,
                process.Status);
        }
    }
}
