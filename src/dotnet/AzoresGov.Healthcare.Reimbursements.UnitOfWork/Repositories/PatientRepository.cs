using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Migrations;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class PatientRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, Patient>, IPatientRepository
    {
        public PatientRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<Patient>> GetAllByFullSearchCriteriaAsync(string filter, int skip, int take, CancellationToken ct)
        {
            return await GetQueryableByFullSearchCriteria(filter)
                .Skip(skip)
                .Take(take)
                .OrderBy(e => e.Name)
                    .ThenBy(e => e.Birth)
                        .ThenBy(e => e.Id)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyCollection<Patient>> GetAllByPatientNumberSearchCriteriaAsync(string filter, int skip, int take, CancellationToken ct)
        {
            return await GetQueryableByPatientNumberSearchCriteria(filter)
                .Skip(skip)
                .Take(take)
                .OrderBy(e => e.Name)
                    .ThenBy(e => e.Birth)
                        .ThenBy(e => e.Id)
                .ToListAsync(ct);
        }

        public Task<int> GetCountByFullSearchCriteriaAsync(string filter, CancellationToken ct)
        {
            return GetQueryableByFullSearchCriteria(filter).CountAsync(ct);
        }

        public Task<int> GetCountByPatientNumberSearchCriteriaAsync(string filter, CancellationToken ct)
        {
            return GetQueryableByPatientNumberSearchCriteria(filter).CountAsync(ct);
        }

        private IQueryable<Patient> GetQueryableByFullSearchCriteria(string filter)
        {
            var patternExpression = $"%{filter.Replace(' ', '%')}%";

            return Entities.Where(e =>
                EF.Functions.Like(e.Name, patternExpression) ||
                EF.Functions.Like(e.Number, patternExpression) ||
                EF.Functions.Like(e.TaxNumber, patternExpression) ||
                EF.Functions.Like(e.PhoneNumber, patternExpression) ||
                EF.Functions.Like(e.MobileNumber, patternExpression) ||
                EF.Functions.Like(e.EmailAddress, patternExpression));
        }

        private IQueryable<Patient> GetQueryableByPatientNumberSearchCriteria(string filter) =>

            Entities.Where(e => e.Number == filter);
    }
}
