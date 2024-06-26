﻿using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<IReadOnlyCollection<Patient>> GetAllByFullSearchCriteriaAsync(
            string filter,
            int skip,
            int take,
            CancellationToken ct);

        Task<IReadOnlyCollection<Patient>> GetAllByPatientIdAsync(
            IReadOnlyCollection<long> patientIds,
            CancellationToken ct);

        Task<IReadOnlyCollection<Patient>> GetAllByPatientNumberCriteriaAsync(
            string filter,
            CancellationToken ct);

        Task<int> GetCountByFullSearchCriteriaAsync(
            string filter,
            CancellationToken ct);

        Task<int> GetCountByPatientNumberCriteriaAsync(
            string filter,
            CancellationToken ct);
    }
}
