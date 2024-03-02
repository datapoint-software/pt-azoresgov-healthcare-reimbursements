using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
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

        public Task<int> CountByEntitySearchCriteriaAsync(
            long entityId,
            string? filter,
            CancellationToken ct)
        {
            var queryable = GetQueryableByUserEntitySearchCriteria(
                entityId,
                filter);

            return queryable.CountAsync(ct);
        }

        public async Task<IReadOnlyCollection<Patient>> GetAllByEntitySearchCriteriaAsync(
            long entityId,
            string? filter,
            int skip,
            int take,
            CancellationToken ct)
        {
            var queryable = GetQueryableByUserEntitySearchCriteria(
                entityId,
                filter);

            return await queryable
                .OrderBy(e => e.Name)
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
        }

        private IQueryable<Patient> GetQueryableByUserEntitySearchCriteria(
            long entityId,
            string? filter)
        {
            var queryable = UnitOfWork.PatientEntities
                .Where(pe => pe.EntityId == entityId)
                .Select(pe => pe.Patient);

            if (!string.IsNullOrEmpty(filter))
            {
                var filterExpression = $"%{filter.Replace(' ', '%')}%";

                queryable = queryable.Where(e => 
                    EF.Functions.Like(e.Name, filterExpression) ||
                    EF.Functions.Like(e.HealthNumber, filterExpression) ||
                    EF.Functions.Like(e.TaxNumber, filterExpression) ||
                    EF.Functions.Like(e.FaxNumber, filter) ||
                    EF.Functions.Like(e.MobileNumber, filter) ||
                    EF.Functions.Like(e.PhoneNumber, filter));
            }

            return queryable;
        }
    }
}