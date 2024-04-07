using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class EntityParentEntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, EntityParentEntity>, IEntityParentEntityRepository
    {
        public EntityParentEntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
