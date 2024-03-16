using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureFamilyIncomeStatementDeleteCommandHandler : ICommandHandler<ProcessCaptureFamilyIncomeStatementDeleteCommand, ProcessCaptureFamilyIncomeStatementDeleteResult>
    {
        private readonly IProcessPatientFamilyIncomeStatementRepository _processPatientFamilyIncomeStatements;

        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public async Task<ProcessCaptureFamilyIncomeStatementDeleteResult> HandleCommandAsync(
            ProcessCaptureFamilyIncomeStatementDeleteCommand command, 
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

            var processPatientFamilyIncomeStatement = await _processPatientFamilyIncomeStatements.GetByProcessIdAsync(
                process.Id,
                ct);
            
            Assert.Found(processPatientFamilyIncomeStatement);
            
            Assert.RowVersion(
                processPatientFamilyIncomeStatement.RowVersionId,
                command.ProcessPatientFamilyIncomeStatementRowVersionId);
            
            _processPatientFamilyIncomeStatements.Remove(
                processPatientFamilyIncomeStatement);

            process.RowVersionId = Guid.NewGuid();

            return new ProcessCaptureFamilyIncomeStatementDeleteResult(
                process.RowVersionId);
        }
    }
}