using AzoresGov.Healthcare.Reimbursements.Enumerations;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Workers.Features.ProcessCreation
{
    public interface IProcessCreationEntitySearchWorker : IWorker
    {
        Task<IReadOnlyCollection<ProcessCreationEntitySearchResult>> SearchUserEntitiesWithPermissionGrantAsync(
            long userId, 
            long permissionId,
            IReadOnlyCollection<EntityNature> nature, 
            string? filter, 
            int skip, 
            int take, 
            CancellationToken ct);
    }
}