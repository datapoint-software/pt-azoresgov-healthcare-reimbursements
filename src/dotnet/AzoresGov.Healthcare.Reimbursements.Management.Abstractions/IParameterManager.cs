using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public interface IParameterManager
    {
        Task<bool> GetPersistentSessionsEnabledAsync(CancellationToken ct);

        Task<int> GetSignInDelayInMillisecondsAsync(CancellationToken ct);

        Task<int> GetUserPasswordHashWorkFactorAsync(CancellationToken ct);

        Task<int> GetUserSessionExpirationInSecondsAsync(CancellationToken ct);
    }
}
