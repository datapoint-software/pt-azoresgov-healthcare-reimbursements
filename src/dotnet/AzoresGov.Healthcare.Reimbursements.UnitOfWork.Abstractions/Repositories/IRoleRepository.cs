﻿using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<IReadOnlyCollection<Role>> GetAllByUserIdAsync(long userId, CancellationToken ct);
    }
}