using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<IReadOnlyCollection<Patient>> GetAllByFullSearchCriteriaAsync(
            string filter,
            int skip,
            int take,
            CancellationToken ct)
        {
            return await GetFullSearchCriteriaQueryable(filter)
                .OrderBy(e => e.Name)
                    .ThenBy(e => e.Birth)
                        .ThenBy(e => e.Id)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyCollection<Patient>> GetAllByPatientIdAsync(IReadOnlyCollection<long> patientIds, CancellationToken ct)
        {
            return await Entities
                .Where(e => patientIds.Contains(e.Id))
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyCollection<Patient>> GetAllByPatientNumberCriteriaAsync(
            string filter,
            CancellationToken ct)
        {
            return await GetPatientNumberCriteriaQueryable(filter)
                .OrderBy(e => e.Name)
                    .ThenBy(e => e.Birth)
                        .ThenBy(e => e.Id)
                .ToListAsync(ct);
        }

        public Task<int> GetCountByFullSearchCriteriaAsync(
            string filter,
            CancellationToken ct)
        {
            return GetFullSearchCriteriaQueryable(filter)
                .CountAsync(ct);
        }

        public Task<int> GetCountByPatientNumberCriteriaAsync(
            string filter,
            CancellationToken ct)
        {
            return GetPatientNumberCriteriaQueryable(filter)
                .CountAsync(ct);
        }

        private IQueryable<Patient> GetFullSearchCriteriaQueryable(string filter)
        {
            var filterExpression = $"%{string.Join('%', filter.Split(' ', StringSplitOptions.RemoveEmptyEntries))}%";

            return Entities.Where(e =>
                EF.Functions.Like(e.Name, filterExpression) ||
                EF.Functions.Like(e.Number, filterExpression) ||
                EF.Functions.Like(e.TaxNumber, filterExpression) ||
                EF.Functions.Like(e.FaxNumber, filterExpression) ||
                EF.Functions.Like(e.MobileNumber, filterExpression) ||
                EF.Functions.Like(e.PhoneNumber, filterExpression) ||
                EF.Functions.Like(e.EmailAddress, filterExpression));
        }

        private IQueryable<Patient> GetPatientNumberCriteriaQueryable(string filter)
        {
            return Entities.Where(e => e.Number == filter);
        }
    }
}
