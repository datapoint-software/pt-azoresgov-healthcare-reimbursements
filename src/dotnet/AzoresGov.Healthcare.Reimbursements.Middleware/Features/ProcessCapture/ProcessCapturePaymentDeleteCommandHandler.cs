using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCapturePaymentDeleteCommandHandler : ICommandHandler<ProcessCapturePaymentDeleteCommand, ProcessCapturePaymentDeleteResult>
    {
        private readonly IProcessPaymentConfigurationRepository _processPaymentSettings;

        private readonly IProcessPaymentWireTransferConfigurationRepository _processPaymentWireTransferSettings;
        
        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;
        
        public async Task<ProcessCapturePaymentDeleteResult> HandleCommandAsync(
            ProcessCapturePaymentDeleteCommand command,
            CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                command.UserId,
                ct);

            Assert.Found(user);

            var process = await _processes.GetByPublicIdAsync(
                command.ProcessId,
                ct);

            Assert.Found(process);

            Assert.ProcessStatus(
                ProcessStatus.Capture,
                process.Status);

            Assert.RowVersion(
                process.RowVersionId,
                command.ProcessRowVersionId);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user.Id,
                process.EntityId,
                ct);

            var processPaymentConfiguration = await _processPaymentSettings.GetByProcessIdAsync(
                process.Id,
                ct);
            
            Assert.Found(processPaymentConfiguration);
            
            Assert.RowVersion(
                processPaymentConfiguration.RowVersionId,
                command.ProcessPaymentConfigurationRowVersionId);
            
            _processPaymentSettings.Remove(processPaymentConfiguration);
                
            if (processPaymentConfiguration.Method == PaymentMethod.WireTransfer)
            {
                var processPaymentWireTransferConfiguration = await _processPaymentWireTransferSettings.GetByProcessIdAsync(
                    process.Id,
                    ct);
                
                Assert.Found(processPaymentWireTransferConfiguration);
                
                Assert.RowVersion(
                    processPaymentWireTransferConfiguration.RowVersionId,
                    command.ProcessPaymentWireTransferConfigurationRowVersionId);
                
                _processPaymentWireTransferSettings.Remove(processPaymentWireTransferConfiguration);
            }

            process.RowVersionId = Guid.NewGuid();

            return new ProcessCapturePaymentDeleteResult(
                process.RowVersionId);
        }
    }
}