using AzoresGov.Healthcare.Reimbursements.Enumerations;
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

        public Task<int> CountBySearchCriteriaAsync(
            string? filter,
            IReadOnlyCollection<long>? entityId,
            IReadOnlyCollection<ProcessStatus>? status,
            CancellationToken ct)
        {
            var queryable = GetQueryableBySearchCriteria(
                filter,
                entityId,
                status);

            return queryable.CountAsync(ct);
        }

        public async Task<IReadOnlyCollection<Process>> GetAllBySearchCriteriaAsync(
            string? filter,
            IReadOnlyCollection<long>? entityId,
            IReadOnlyCollection<ProcessStatus>? status,
            int skip,
            int take,
            CancellationToken ct)
        {
            var queryable = GetQueryableBySearchCriteria(
                filter,
                entityId,
                status);

            return await queryable
                .OrderBy(e => e.Touch)
                .ThenBy(e => e.Creation)
                .ThenBy(e => e.Patient.Name)
                .ThenBy(e => e.Entity.Name)
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
        }

        private IQueryable<Process> GetQueryableBySearchCriteria(
            string? filter,
            IReadOnlyCollection<long>? entityId,
            IReadOnlyCollection<ProcessStatus>? status)
        {
            var queryable = Entities.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                var filterExpression = $"%{filter.Replace(' ', '%')}%";

                queryable = queryable.Where(p => 
                    EF.Functions.Like(p.Number, filterExpression) ||
                    EF.Functions.Like(p.Entity.Name, filterExpression) ||
                    EF.Functions.Like(p.Entity.Code, filterExpression) ||
                    EF.Functions.Like(p.Patient.Name, filterExpression) ||
                    EF.Functions.Like(p.Patient.HealthNumber, filterExpression) ||
                    EF.Functions.Like(p.Patient.TaxNumber, filterExpression) ||
                    EF.Functions.Like(p.Patient.EmailAddress, filterExpression) ||
                    EF.Functions.Like(p.Patient.FaxNumber, filterExpression) ||
                    EF.Functions.Like(p.Patient.MobileNumber, filterExpression) ||
                    EF.Functions.Like(p.Patient.PhoneNumber, filterExpression));
            }

            if (entityId is not null)
                queryable = queryable.Where(e => entityId.Contains(e.EntityId));

            if (status is not null)
                queryable = queryable.Where(e => status.Contains(e.Status));

            return queryable;
        }
    }
}