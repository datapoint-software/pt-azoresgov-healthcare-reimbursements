namespace AzoresGov.Healthcare.Reimbursements.Management.Helpers
{
    internal static class CacheHelper
    {
        internal static string GenerateDistributedCacheKey(string name) =>

            $"azoresgov-hrms:{name}";

        internal static string GenerateProcessNumberSequenceCacheKey(long entityId, int processYear) =>

            $"{GenerateDistributedCacheKey("process-number-sequence")}/{entityId}/{processYear}";
    }
}
