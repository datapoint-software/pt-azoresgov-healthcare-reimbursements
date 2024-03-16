using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureFamilyIncomeStatementCommandHandler : ICommandHandler<ProcessCaptureFamilyIncomeStatementCommand, ProcessCaptureFamilyIncomeStatementResult>
    {
        private readonly IProcessPatientFamilyIncomeStatementRepository _processPatientFamilyIncomeStatements;

        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public ProcessCaptureFamilyIncomeStatementCommandHandler(IProcessPatientFamilyIncomeStatementRepository processPatientFamilyIncomeStatements, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _processPatientFamilyIncomeStatements = processPatientFamilyIncomeStatements;
            _processes = processes;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCaptureFamilyIncomeStatementResult> HandleCommandAsync(
            ProcessCaptureFamilyIncomeStatementCommand command,
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

            var processPatientFamilyIncomeStatement = await GetOrCreateProcessPatientFamilyIncomeStatement(
                process,
                command.ProcessPatientFamilyIncomeStatementRowVersionId,
                ct);

            processPatientFamilyIncomeStatement.Year = command.Year;
            processPatientFamilyIncomeStatement.AccessCode = command.AccessCode;
            processPatientFamilyIncomeStatement.FamilyMemberCount = command.FamilyMemberCount;
            processPatientFamilyIncomeStatement.FamilyIncome = command.FamilyIncome;

            process.RowVersionId = Guid.NewGuid();
            processPatientFamilyIncomeStatement.RowVersionId = Guid.NewGuid();

            return new ProcessCaptureFamilyIncomeStatementResult(
                process.RowVersionId,
                processPatientFamilyIncomeStatement.RowVersionId);
        }

        private async Task<ProcessPatientFamilyIncomeStatement> GetOrCreateProcessPatientFamilyIncomeStatement(
            Process process,
            Guid? processPatientFamilyIncomeStatementRowVersionId,
            CancellationToken ct)
        {
            var processPatientFamilyIncomeStatement = await _processPatientFamilyIncomeStatements.GetByProcessIdAsync(
                process.Id,
                ct);

            if (processPatientFamilyIncomeStatement is null)
            {
                return _processPatientFamilyIncomeStatements.Add(new ProcessPatientFamilyIncomeStatement()
                {
                    Process = process
                });
            }
            
            Assert.RowVersion(
                processPatientFamilyIncomeStatement.RowVersionId,
                processPatientFamilyIncomeStatementRowVersionId);

            return processPatientFamilyIncomeStatement;
        }
    }
}