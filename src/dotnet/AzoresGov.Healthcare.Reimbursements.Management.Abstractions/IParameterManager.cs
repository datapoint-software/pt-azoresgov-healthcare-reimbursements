using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public interface IParameterManager
    {
        Task<bool> GetBasicAuthenticationEnabledAsync(CancellationToken ct);

        Task<bool> GetBasicAuthenticationPersistentSessionsEnabledAsync(CancellationToken ct);

        Task<int> GetBasicAuthenticationDelayInMillisecondsAsync(CancellationToken ct);

        Task<int> GetProcessExpirationInDaysAsync(CancellationToken ct);

        Task<int> GetUserSessionExpirationInSecondsAsync(CancellationToken ct);

        Task<int> GetUserPasswordHashWorkFactorAsync(CancellationToken ct);
    }
}
