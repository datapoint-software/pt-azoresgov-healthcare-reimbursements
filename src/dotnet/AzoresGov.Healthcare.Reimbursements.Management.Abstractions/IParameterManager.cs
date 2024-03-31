using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public interface IParameterManager
    {
        Task<bool> GetPersistentSessionsEnabledAsync(CancellationToken ct);
    }
}
