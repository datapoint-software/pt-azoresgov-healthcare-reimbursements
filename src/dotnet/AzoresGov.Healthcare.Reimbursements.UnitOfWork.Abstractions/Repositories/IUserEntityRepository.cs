﻿using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserEntityRepository : IRepository<UserEntity>
    {
        Task<bool> AnyByUserIdAndEntityIdAsync(long userId, long entityId, CancellationToken ct);

        Task<IReadOnlyCollection<long>> GetAllEntityIdByUserIdAsync(long userId, CancellationToken ct);
    }
}
