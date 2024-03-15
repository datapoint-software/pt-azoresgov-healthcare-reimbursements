using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureWriteLegalRepresentativeCommandHandler : ICommandHandler<ProcessCaptureWriteLegalRepresentativeCommand, ProcessCaptureWriteLegalRepresentativeResult>
    {
        private readonly IProcessPatientLegalRepresentativeRepository _processPatientLegalRepresentatives;
        
        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCaptureWriteLegalRepresentativeCommandHandler(IProcessPatientLegalRepresentativeRepository processPatientLegalRepresentatives, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _processPatientLegalRepresentatives = processPatientLegalRepresentatives;
            _processes = processes;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCaptureWriteLegalRepresentativeResult> HandleCommandAsync(
            ProcessCaptureWriteLegalRepresentativeCommand command, 
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

            var processPatientLegalRepresentative = await _processPatientLegalRepresentatives.GetByProcessIdAsync(
                process.Id,
                ct);
            
            Assert.Found(processPatientLegalRepresentative);
            
            Assert.RowVersion(
                processPatientLegalRepresentative.RowVersionId,
                command.ProcessPatientLegalRepresentativeId);

            processPatientLegalRepresentative.Name = command.Name;
            processPatientLegalRepresentative.TaxNumber = command.TaxNumber;
            processPatientLegalRepresentative.EmailAddress = command.EmailAddress;
            processPatientLegalRepresentative.FaxNumber = command.FaxNumber;
            processPatientLegalRepresentative.MobileNumber = command.MobileNumber;
            processPatientLegalRepresentative.PhoneNumber = command.PhoneNumber;
            
            process.RowVersionId = Guid.NewGuid();
            processPatientLegalRepresentative.RowVersionId = Guid.NewGuid();

            return new ProcessCaptureWriteLegalRepresentativeResult(
                process.RowVersionId,
                processPatientLegalRepresentative.RowVersionId);
        }
    }
}