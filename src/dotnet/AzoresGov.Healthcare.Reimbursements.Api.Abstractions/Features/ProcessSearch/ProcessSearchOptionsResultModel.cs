using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessSearch
{
    public sealed class ProcessSearchOptionsResultModel
    {
        public ProcessSearchOptionsResultModel(IReadOnlyCollection<ProcessSearchOptionsEntityResultModel> entities)
        {
            Entities = entities;
        }

        public IReadOnlyCollection<ProcessSearchOptionsEntityResultModel> Entities { get; }
    }
}