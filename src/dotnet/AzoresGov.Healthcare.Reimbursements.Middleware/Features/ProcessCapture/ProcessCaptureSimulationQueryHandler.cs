using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureSimulationQueryHandler : IQueryHandler<ProcessCaptureSimulationQuery, ProcessCaptureSimulationResult>
    {
        private readonly IIasConfigurationRepository _iasSettings;

        private readonly IProcessRepository _processes;

        private readonly IProcessConfigurationRepository _processSettings;

        private readonly IProcessPatientFamilyIncomeStatementRepository _processPatientFamilyIncomeStatements;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public async Task<ProcessCaptureSimulationResult> HandleQueryAsync(ProcessCaptureSimulationQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                query.UserId,
                ct);

            Assert.Found(user);

            var process = await _processes.GetByPublicIdAsync(
                query.ProcessId,
                ct);

            Assert.Found(process);

            Assert.ProcessStatus(
                ProcessStatus.Capture,
                process.Status);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user.Id,
                process.EntityId,
                ct);

            var iasConfiguration = await _iasSettings.GetByYearAsync(
                query.Creation.Year,
                ct);

            Assert.Found(iasConfiguration);

            var processConfiguration = await _processSettings.GetByProcessIdAsync(
                process.Id,
                ct);

            var processPatientFamilyIncomeStatement = await _processPatientFamilyIncomeStatements.GetByProcessIdAsync(
                process.Id,
                ct);

            var multiplier = ProcessHelper.CalculateMultiplier(
                iasConfiguration,
                processConfiguration,
                processPatientFamilyIncomeStatement);

            var lines = new List<ProcessCaptureSimulationLineResult>();

            if (processConfiguration is not null)
            {
                if (processConfiguration.MachadoJosephEnabled)
                    lines.Add(new ProcessCaptureSimulationLineResult("Machado-Joseph", null));

                if (processConfiguration.ReimbursementLimitBypassEnabled)
                    lines.Add(new ProcessCaptureSimulationLineResult("Reembolso sem limite de quantidades", null));

                if (processConfiguration.UnemploymentEnabled)
                    lines.Add(new ProcessCaptureSimulationLineResult("Situação de desemprego", null));

                if (lines.Count > 0)
                    return new ProcessCaptureSimulationResult(lines, multiplier);
            }

            if (processPatientFamilyIncomeStatement is null)
            {
                lines.Add(new ProcessCaptureSimulationLineResult("Declaração de rendimentos", null));

                return new ProcessCaptureSimulationResult(lines, multiplier);
            }

            var ias = iasConfiguration.Amount;
            var raaf = processPatientFamilyIncomeStatement.FamilyIncome;
            var ag = processPatientFamilyIncomeStatement.FamilyMemberCount;

            var r = (raaf / ag / ias / 12);

            lines.Add(new ProcessCaptureSimulationLineResult(
                "IAS", 
                iasConfiguration.Amount));

            lines.Add(new ProcessCaptureSimulationLineResult(
                "AG",
                processPatientFamilyIncomeStatement.FamilyMemberCount));

            lines.Add(new ProcessCaptureSimulationLineResult(
                "RAAF",
                processPatientFamilyIncomeStatement.FamilyIncome));

            lines.Add(new ProcessCaptureSimulationLineResult(
                "R",
                Math.Round(r, 4)));

            lines.Add(new ProcessCaptureSimulationLineResult(
                "R = (RAAF / AG / IAS / 12) <=>",
                null));

            lines.Add(new ProcessCaptureSimulationLineResult(
                $"R = ({processPatientFamilyIncomeStatement.FamilyIncome} / {processPatientFamilyIncomeStatement.FamilyMemberCount} / {iasConfiguration.Amount} / 12) <=>",
                null));

            return new ProcessCaptureSimulationResult(
                lines,
                multiplier);
        }
    }
}
