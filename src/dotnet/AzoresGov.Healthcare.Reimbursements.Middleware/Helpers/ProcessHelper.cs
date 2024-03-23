using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class ProcessHelper
    {
        internal static decimal CalculateMultiplier(IasConfiguration iasConfiguration, ProcessConfiguration? processConfiguration, ProcessPatientFamilyIncomeStatement? processPatientFamilyIncomeStatement)
        {
            if (processConfiguration is not null && (
                processConfiguration.MachadoJosephEnabled ||
                processConfiguration.ReimbursementLimitBypassEnabled ||
                processConfiguration.UnemploymentEnabled))
                return 1;

            if (processPatientFamilyIncomeStatement is null)
                return .4m;

            var ias = iasConfiguration.Amount;
            var raaf = processPatientFamilyIncomeStatement.FamilyIncome;
            var ag = processPatientFamilyIncomeStatement.FamilyMemberCount;

            var r = (raaf / ag / ias / 12);

            if (r < 2.5m)
                return 1m;

            if (r <= 4.5m)
                return .8m;

            return .4m;
        }
    }
}
