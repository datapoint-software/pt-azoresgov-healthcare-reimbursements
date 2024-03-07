using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessSearch
{
    public sealed class ProcessSearchOptionsResult
    {
        public ProcessSearchOptionsResult(IReadOnlyCollection<ProcessSearchOptionsEntityResult> entities)
        {
            Entities = entities;
        }

        public IReadOnlyCollection<ProcessSearchOptionsEntityResult> Entities { get; }
    }
}