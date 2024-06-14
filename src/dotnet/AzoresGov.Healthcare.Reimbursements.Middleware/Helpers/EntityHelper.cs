using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class EntityHelper
    {
        internal static async Task<string> GetSequenceNameAsync(IEntityRepository entities, Entity entity, CancellationToken ct)
        {
            var tokens = await entities.GetAllParentEntityCodesByEntityIdWithOrderByLevelAsync(
                entity.Id, 
                ct);

            var sb = new StringBuilder();

            foreach (var token in tokens)
                sb.Append(token.ToUpper()).Append('/');

            sb.Append(entity.Code.ToUpper());

            return sb.ToString();
        }
    }
}
