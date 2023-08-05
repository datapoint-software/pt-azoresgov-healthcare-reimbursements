using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class EntityParentRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, EntityParentEntity>, IEntityParentRepository
    {
        public EntityParentRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
