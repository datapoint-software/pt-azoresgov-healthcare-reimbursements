using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCapturePaymentCommandHandler : ICommandHandler<ProcessCapturePaymentCommand, ProcessCapturePaymentResult>
    {
        private readonly IProcessPaymentConfigurationRepository _processPaymentSettings;

        private readonly IProcessPaymentWireTransferConfigurationRepository _processPaymentWireTransferSettings;
        
        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public ProcessCapturePaymentCommandHandler(IProcessPaymentConfigurationRepository processPaymentSettings, IProcessPaymentWireTransferConfigurationRepository processPaymentWireTransferSettings, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _processPaymentSettings = processPaymentSettings;
            _processPaymentWireTransferSettings = processPaymentWireTransferSettings;
            _processes = processes;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCapturePaymentResult> HandleCommandAsync(ProcessCapturePaymentCommand command, CancellationToken ct)
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

            var processPaymentConfiguration = await GetOrCreateProcessPaymentConfigurationAsync(
                process,
                command.ProcessPaymentConfigurationRowVersionId,
                ct);

            processPaymentConfiguration.Method = command.Method;
            processPaymentConfiguration.Receiver = command.Receiver;

            var processPaymentWireTransferConfiguration = await _processPaymentWireTransferSettings.GetByProcessIdAsync(
                process.Id,
                ct);

            if (processPaymentConfiguration.Method == PaymentMethod.WireTransfer)
            {
                if (processPaymentWireTransferConfiguration is not null)
                {
                    Assert.RowVersion(
                        processPaymentWireTransferConfiguration.RowVersionId,
                        command.ProcessPaymentWireTransferConfigurationRowVersionId);
                }
                else
                {
                    processPaymentWireTransferConfiguration = _processPaymentWireTransferSettings.Add(new ProcessPaymentWireTransferConfiguration()
                    {
                        Process = process
                    });
                }
                
                processPaymentWireTransferConfiguration.Iban = command.Iban!;
                processPaymentWireTransferConfiguration.Swift = command.Swift!;
            }
            
            else if (processPaymentWireTransferConfiguration is not null)
            {
                _processPaymentWireTransferSettings.Remove(
                    processPaymentWireTransferConfiguration);
            }

            process.RowVersionId = Guid.NewGuid();
            processPaymentConfiguration.RowVersionId = Guid.NewGuid();
            
            if (processPaymentWireTransferConfiguration is not null)
                processPaymentWireTransferConfiguration.RowVersionId = Guid.NewGuid();

            return new ProcessCapturePaymentResult(
                process.RowVersionId,
                processPaymentConfiguration.RowVersionId,
                processPaymentWireTransferConfiguration?.RowVersionId);
        }

        private async Task<ProcessPaymentConfiguration> GetOrCreateProcessPaymentConfigurationAsync(
            Process process,
            Guid? processPaymentConfigurationRowVersionId,
            CancellationToken ct)
        {
            var processPaymentConfiguration = await _processPaymentSettings.GetByProcessIdAsync(
                process.Id,
                ct);

            if (processPaymentConfiguration is null)
            {
                return _processPaymentSettings.Add(new ProcessPaymentConfiguration()
                {
                    Process = process
                });
            }
            
            Assert.RowVersion(
                processPaymentConfiguration.RowVersionId,
                processPaymentConfigurationRowVersionId);

            return processPaymentConfiguration;
        }
    }
}