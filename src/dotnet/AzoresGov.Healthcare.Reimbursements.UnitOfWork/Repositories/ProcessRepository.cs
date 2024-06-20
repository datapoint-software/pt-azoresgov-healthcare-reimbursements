using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class ProcessRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, Process>, IProcessRepository
    {
        public ProcessRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<Process>> GetAllByBasicSearchCriteriaAsync(IReadOnlyCollection<long> entityIds, string filter, int skip, int take, CancellationToken ct)
        {
            return await GetBasicSearchCriteria(entityIds, filter)
                .OrderBy(e => e.Creation)
                    .ThenBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
        }
       
        public async Task<IReadOnlyCollection<Process>> GetAllByEmptySearchCriteriaAsync(IReadOnlyCollection<long> entityIds, int skip, int take, CancellationToken ct)
        {
            return await GetEmptySearchCriteria(entityIds)
                .OrderBy(e => e.Creation)
                    .ThenBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
        }
        
        public async Task<IReadOnlyCollection<Process>> GetAllByFullSearchCriteriaAsync(IReadOnlyCollection<long> entityIds, string filter, int skip, int take, CancellationToken ct)
        {
            return await GetFullSearchCriteria(entityIds, filter)
                .OrderBy(e => e.Creation)
                    .ThenBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
        }
        
        public Task<int> GetCountByBasicSearchCriteriaAsync(IReadOnlyCollection<long> entityIds, string filter, CancellationToken ct)
        {
            return GetBasicSearchCriteria(entityIds, filter)
                .CountAsync(ct);
        }
        
        public Task<int> GetCountByEmptySearchCriteriaAsync(IReadOnlyCollection<long> entityIds, CancellationToken ct)
        {
            return GetEmptySearchCriteria(entityIds)
                .CountAsync(ct);
        }

        public Task<int> GetCountByFullSearchCriteriaAsync(IReadOnlyCollection<long> entityIds, string filter, CancellationToken ct)
        {
            return GetFullSearchCriteria(entityIds, filter)
                .CountAsync(ct);
        }

        private IQueryable<Process> GetBasicSearchCriteria(IReadOnlyCollection<long> entityIds, string filter)
        {
            var filterExpression = $"%{string.Join('%', filter.Split(' ', System.StringSplitOptions.RemoveEmptyEntries))}%";

            return GetEmptySearchCriteria(entityIds)
                .Where(e => EF.Functions.Like(e.Number, filterExpression) || 
                    e.Patient.Number == filter || 
                    e.Patient.TaxNumber == filter);
        }

        private IQueryable<Process> GetEmptySearchCriteria(IReadOnlyCollection<long> entityIds)
        {
            return Entities.Where(e => entityIds.Contains(e.EntityId));
        }

        private IQueryable<Process> GetFullSearchCriteria(IReadOnlyCollection<long> entityIds, string filter)
        {
            var filterExpression = $"%{string.Join('%', filter.Split(' ', System.StringSplitOptions.RemoveEmptyEntries))}%";

            return GetEmptySearchCriteria(entityIds)
                .Where(e => EF.Functions.Like(e.Number, filterExpression) ||
                    EF.Functions.Like(e.Entity.Code, filterExpression) ||
                    EF.Functions.Like(e.Entity.Name, filterExpression) ||
                    EF.Functions.Like(e.Patient.Name, filterExpression) ||
                    EF.Functions.Like(e.Patient.Number, filterExpression) ||
                    EF.Functions.Like(e.Patient.TaxNumber, filterExpression) ||
                    EF.Functions.Like(e.Patient.FaxNumber, filterExpression) ||
                    EF.Functions.Like(e.Patient.MobileNumber, filterExpression) ||
                    EF.Functions.Like(e.Patient.PhoneNumber, filterExpression) ||
                    EF.Functions.Like(e.Patient.EmailAddress, filterExpression));
        }
    }
}
