using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureDeleteLegalRepresentativeCommandHandler : ICommandHandler<ProcessCaptureLegalRepresentativeDeleteCommand, ProcessCaptureLegalRepresentativeDeleteResult>
    {
        private readonly IProcessPatientLegalRepresentativeRepository _processPatientLegalRepresentatives;
        
        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCaptureDeleteLegalRepresentativeCommandHandler(IProcessPatientLegalRepresentativeRepository processPatientLegalRepresentatives, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _processPatientLegalRepresentatives = processPatientLegalRepresentatives;
            _processes = processes;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCaptureLegalRepresentativeDeleteResult> HandleCommandAsync(ProcessCaptureLegalRepresentativeDeleteCommand command, CancellationToken ct)
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

            var processPatientLegalRepresentative = await _processPatientLegalRepresentatives.GetByProcessIdAsync(
                process.Id,
                ct);
            
            Assert.Found(processPatientLegalRepresentative);
            
            _processPatientLegalRepresentatives.Remove(
                processPatientLegalRepresentative);

            return new ProcessCaptureLegalRepresentativeDeleteResult(
                process.RowVersionId);
        }
    }
}