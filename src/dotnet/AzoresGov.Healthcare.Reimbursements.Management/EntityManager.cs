using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Globalization;
using Microsoft.Extensions.Caching.Distributed;
using AzoresGov.Healthcare.Reimbursements.Management.Helpers;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public sealed class EntityManager : IEntityManager
    {
        private readonly IDistributedCache _distributedCache;

        private readonly IEntityRepository _entities;

        public EntityManager(IDistributedCache distributedCache, IEntityRepository entities)
        {
            _distributedCache = distributedCache;
            _entities = entities;
        }

        public async Task<string> GetProcessNumberSequenceNameAsync(Entity entity, int processYear, CancellationToken ct)
        {
            var ck = CacheHelper.GenerateProcessNumberSequenceCacheKey(
                entity.Id, 
                processYear);

            var result = await _distributedCache.GetStringAsync(ck, ct);

            if (string.IsNullOrEmpty(result))
            {
                var sb = new StringBuilder(processYear.ToString(CultureInfo.InvariantCulture));

                sb.Append("/");

                var tokens = await _entities.GetAllParentEntityCodesByEntityIdWithOrderByLevelAsync(
                    entity.Id,
                    ct);

                foreach (var token in tokens)
                {
                    sb.Append(token.ToUpper());
                    sb.Append('/');
                }

                sb.Append(entity.Code);

                result = sb.ToString();

                await _distributedCache.SetStringAsync(ck, result, ct);
            }

            return result;
        }
    }
}
