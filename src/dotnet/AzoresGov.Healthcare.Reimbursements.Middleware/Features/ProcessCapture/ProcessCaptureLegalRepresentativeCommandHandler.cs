using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureLegalRepresentativeCommandHandler : ICommandHandler<ProcessCaptureLegalRepresentativeCommand, ProcessCaptureLegalRepresentativeResult>
    {
        private readonly IProcessPatientLegalRepresentativeRepository _processPatientLegalRepresentatives;
        
        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCaptureLegalRepresentativeCommandHandler(IProcessPatientLegalRepresentativeRepository processPatientLegalRepresentatives, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _processPatientLegalRepresentatives = processPatientLegalRepresentatives;
            _processes = processes;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCaptureLegalRepresentativeResult> HandleCommandAsync(
            ProcessCaptureLegalRepresentativeCommand command, 
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

            var processPatientLegalRepresentative = await GetOrCreateProcessPatientLegalRepresentativeAsync(
                process,
                command.ProcessPatientLegalRepresentativeId,
                ct);

            processPatientLegalRepresentative.Name = command.Name;
            processPatientLegalRepresentative.TaxNumber = command.TaxNumber;
            processPatientLegalRepresentative.EmailAddress = command.EmailAddress;
            processPatientLegalRepresentative.FaxNumber = command.FaxNumber;
            processPatientLegalRepresentative.MobileNumber = command.MobileNumber;
            processPatientLegalRepresentative.PhoneNumber = command.PhoneNumber;
            
            process.RowVersionId = Guid.NewGuid();
            processPatientLegalRepresentative.RowVersionId = Guid.NewGuid();

            return new ProcessCaptureLegalRepresentativeResult(
                process.RowVersionId,
                processPatientLegalRepresentative.RowVersionId);
        }

        private async Task<ProcessPatientLegalRepresentative> GetOrCreateProcessPatientLegalRepresentativeAsync(
            Process process,
            Guid? processPatientLegalRepresentativeRowVersionId,
            CancellationToken ct)
        {
            var processPatientLegalRepresentative = await _processPatientLegalRepresentatives.GetByProcessIdAsync(
                process.Id,
                ct);

            if (processPatientLegalRepresentative is null)
            {
                return this._processPatientLegalRepresentatives.Add(new ProcessPatientLegalRepresentative()
                {
                    Process = process
                });
            }
            
            Assert.RowVersion(
                processPatientLegalRepresentative.RowVersionId,
                processPatientLegalRepresentativeRowVersionId);

            return processPatientLegalRepresentative;
        }
    }
}